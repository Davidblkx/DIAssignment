using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using DIAssignment.Core.Models.Entity;
using DIAssignment.Core.Models.Messages;
using DIAssignment.Core.Services;

namespace DIAssignment.Projection.Actors
{
    /// <summary>
    /// Route messages between artists and other entities
    /// </summary>
    public class ProjectionMaster : ReceiveActor
    {
        private readonly IInjector _injector;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private IActorRef _artistActor = ActorRefs.Nobody;
        private IActorRef _albumActor = ActorRefs.Nobody;

        public ProjectionMaster(IInjector injector)
        {
            _injector = injector;
            Receive<UpsertMessage>(OnEvent);
            Receive<AlbumArtistMessage>(e => GetAlbumActor().Tell(e));
        }

        public void OnEvent(UpsertMessage message)
        {
            if (message.Entity is null) return;

            _log.Info($"Processing [{message.Type}] id: " + message.CollectionId);
            if (message.Entity is Artist a)
                GetArtistActor().Tell(a);
            else if (message.CollectionId > 0)
                GetAlbumActor().Tell(message);
        }

        private IActorRef GetArtistActor()
        {
            if (_artistActor == ActorRefs.Nobody)
                _artistActor = Context.ActorOf(
                    Props.Create<ArtistActor>(_injector).WithRouter(
                        new RoundRobinPool(5)));

            return _artistActor;
        }

        private IActorRef GetAlbumActor()
        {
            if (_albumActor == ActorRefs.Nobody)
                _albumActor = Context.ActorOf(
                    Props.Create<AlbumActor>(_injector).WithRouter(
                        new ConsistentHashingPool(50)));

            return _albumActor;
        }
    }
}
