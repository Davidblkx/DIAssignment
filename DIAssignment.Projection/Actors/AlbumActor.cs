using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using DIAssignment.Core.Infra;
using DIAssignment.Core.Models.Entity;
using DIAssignment.Core.Models.Messages;
using DIAssignment.Core.Services;
using DIAssignment.Projection.Services;
using System.Linq;

namespace DIAssignment.Projection.Actors
{
    // Create or update an album projection 
    public class AlbumActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IMongoService _mongo;
        private readonly IInjector _injector;
        private readonly Cluster _cluster;

        private IActorRef _mongoActor = ActorRefs.Nobody;

        // last used projection
        private Album _projection = new();

        public AlbumActor(IInjector injector)
        {
            _injector = injector;
            _mongo = injector.Get<IMongoService>();
            _cluster = Cluster.Get(Context.System);

            Receive<UpsertMessage>(OnUpsertMessage);
            Receive<AlbumArtistMessage>(OnArtist);
        }

        public void OnArtist(AlbumArtistMessage msg)
        {
            _projection = GetProjection(msg.CollectionId);
            UpdateArtist(msg.Artist);
            SaveProjection(_projection);
        }

        public void OnUpsertMessage(UpsertMessage message)
        {
            _projection = GetProjection(message.CollectionId);

            switch (message.Entity)
            {
                case Collection c:
                    _projection.MergeCollection(c);
                    break;

                case CollectionMatch c:
                    _projection.UPC = c.UPC;
                    break;

                case ArtistCollection a:
                    SearchArtist(a);
                    break;

                default: return;
            }

            SaveProjection(_projection);
        }

        private void SearchArtist(ArtistCollection aCollection)
        {
            var artist = _mongo.GetArtist(aCollection.ArtistId);
            if (artist is null) return;

            UpdateArtist(artist);
        }

        private void UpdateArtist(Artist artist)
        {
            _projection.Artists
                .RemoveAll(e => e.Id == artist.Id);
            _projection.Artists.Add(artist);

            _log.Info($"Added artist {artist.Name} to {_projection.Name}");
        }

        private void SaveProjection(Album album)
        {
            _log.Info("Updating projection for " + album.Id);
            if (_mongoActor == ActorRefs.Nobody)
                _mongoActor = Context.ActorOf(
                    Props.Create<SaveProjectionActor>(_injector));
            _mongoActor.Tell(album);

            SendToElastic(album);
        }

        private void SendToElastic(Album album)
        {
            var target = _cluster.State.Members.First(e => e.HasRole("esentrypoint"));
            var targetUrl = $"{target.Address}/user/{ActorNames.EsEntryPoint}";

            Context.ActorSelection(targetUrl).Tell(album);
        }

        private Album GetProjection(long id)
        {
            if (_projection.Id == id)
                return _projection;

            return _mongo.GetAlbum(id)
                ?? new Album { Id = id };
        }
    }
}
