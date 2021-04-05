using Akka.Actor;
using Akka.Routing;
using DIAssignment.Core.Models.Entity;
using DIAssignment.Core.Services;
using DIAssignment.ESEntryPoint.Services;

namespace DIAssignment.ESEntryPoint.Actors
{
    public class EsEntryPointActor : ReceiveActor, IWithUnboundedStash
    {
        private readonly IInjector _injector;
        private readonly IElasticService _elastic;

        private IActorRef _indexAlbum = ActorRefs.Nobody;

        public IStash Stash { get; set; } = null!;

        public EsEntryPointActor(IInjector injector)
        {
            _injector = injector;
            _elastic = injector.Get<IElasticService>();

            Receive<IActorRef>(e => _indexAlbum = e);
            AwaitElastic();
        }

        public void AwaitElastic()
        {
            Receive<ElasticReady>(_ =>
            {
                _elastic.EnsureIndex<Album>();

                CreateIndexAlbumActor();
                Become(ListenForAlbuns);
                Stash.UnstashAll();
            });
            ReceiveAny(o => Stash.Stash());
        }

        public void ListenForAlbuns()
        {
            Receive<Album>(e => _indexAlbum.Tell(e));
        }

        private void CreateIndexAlbumActor()
        {
            if (_indexAlbum != ActorRefs.Nobody) return;
            _indexAlbum = Context.ActorOf(
                Props.Create<IndexAlbumActor>(_injector)
                .WithRouter(new RoundRobinPool(50)), "indexalbum");
        }

        public class ElasticReady {}
    }
}
