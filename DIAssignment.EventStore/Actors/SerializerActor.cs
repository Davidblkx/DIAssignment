using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using Akka.Routing;
using DIAssignment.Core.Infra;
using DIAssignment.Core.Models.Messages;
using DIAssignment.Core.Services;
using System.Linq;
using static DIAssignment.Core.Models.ImportEntityType;

namespace DIAssignment.EventStore.Actors
{
    /// <summary>
    /// Serializes an entity from a DbFile and sent to storage and to projection
    /// </summary>
    public class SerializerActor : ReceiveActor
    {
        private readonly IInjector _injector;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly Cluster _cluster;

        private IActorRef _mongoActor = ActorRefs.Nobody;

        public SerializerActor(IInjector injector)
        {
            _injector = injector;
            _cluster = Cluster.Get(Context.System);
            Receive<ImportEntityMessage>(OnImportEntity);
        }

        public void OnImportEntity(ImportEntityMessage message)
        {
            var row = message.EntityRow;
            if (row is null) return;

            UpsertMessage toInsert = message.Type switch
            {
                Artist => UpsertArtist.FromRow(row),
                ArtistCollection => UpsertArtistCollection.FromRow(row),
                Collection => UpsertCollection.FromRow(row),
                CollectionMatch => UpsertCollectionMatch.FromRow(row),
                _ => throw new System.NotImplementedException(),
            };

            _log.Info($"New {message.Type} to update");
            SaveUpsert(toInsert);
            UpdateProjection(toInsert);
        }

        private void SaveUpsert(UpsertMessage msg)
        {
            if (_mongoActor == ActorRefs.Nobody)
                _mongoActor = Context.ActorOf(
                    Props.Create<MongoActor>(_injector)
                    .WithRouter(new RoundRobinPool(5)), "mongo_insert");

            _mongoActor.Tell(msg);
        }

        private void UpdateProjection(UpsertMessage msg)
        {
            var target = _cluster.State.Members.First(e => e.HasRole("projection"));
            var targetUrl = $"{target.Address}/user/{ActorNames.Projection}";

            Context.ActorSelection(targetUrl).Tell(msg);
        }
    }
}
