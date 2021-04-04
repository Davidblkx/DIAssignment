using Akka.Actor;
using Akka.Event;
using DIAssignment.Core.Models.Entity;
using DIAssignment.Core.Models.Messages;
using DIAssignment.Core.Services;
using DIAssignment.Projection.Services;

namespace DIAssignment.Projection.Actors
{
    /// <summary>
    /// Match an artist to a collection
    /// </summary>
    public class ArtistActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IMongoService _mongo;

        public ArtistActor(IInjector injector)
        {
            _mongo = injector.Get<IMongoService>();
            Receive<Artist>(OnArtist);
        }

        public void OnArtist(Artist artist)
        {
            _log.Info("Looking for artist collections of " + artist.Id);

            foreach (var collection in _mongo.GetArtistCollections(artist.Id))
                if (collection is ArtistCollection c)
                    Context.Parent.Tell(
                        new AlbumArtistMessage(artist, c.CollectionId));

        }
    }
}
