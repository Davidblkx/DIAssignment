using Akka.Actor;
using Akka.Event;
using DIAssignment.Core.Models.Entity;
using DIAssignment.Core.Services;
using DIAssignment.Projection.Services;

namespace DIAssignment.Projection.Actors
{
    /// <summary>
    /// Saves a album projection to database
    /// </summary>
    public class SaveProjectionActor : ReceiveActor
    {
        private readonly IMongoService _mongo;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public SaveProjectionActor(IInjector injector)
        {
            _mongo = injector.Get<IMongoService>();
            Receive<Album>(OnProjection);
        }

        public void OnProjection(Album album)
        {
            _mongo.UpsertAlbum(album);
            _log.Info($"Updated projection for id: " + album.Id);
        }
    }
}
