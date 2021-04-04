using Akka.Actor;
using Akka.Event;
using DIAssignment.Core.Models.Entity;
using DIAssignment.Core.Services;
using DIAssignment.ESEntryPoint.Services;

namespace DIAssignment.ESEntryPoint.Actors
{
    public class IndexAlbumActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IElasticService _elastic;

        public IndexAlbumActor(IInjector injector)
        {
            _elastic = injector.Get<IElasticService>();
            Receive<Album>(OnAlbum);
        }

        public void OnAlbum(Album album)
        {
            _log.Info($"Adding album [{album.Id}] {album.Name} to index ");
            _elastic.Index(album);
        }
    }
}
