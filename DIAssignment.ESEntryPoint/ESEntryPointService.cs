using Akka.Actor;
using DIAssignment.Core.Actors;
using DIAssignment.Core.Infra;
using DIAssignment.Core.Services;
using DIAssignment.ESEntryPoint.Actors;
using DIAssignment.ESEntryPoint.Services;

namespace DIAssignment.ESEntryPoint
{
    internal sealed class ESEntryPointService : BaseActorService
    {
        public static ESEntryPointService Start() => new();

        protected override void OnStart()
        {
            var injector = new Injector();

            var actor = ActorSystem.ActorOf(
                Props.Create<EsEntryPointActor>(injector),
                ActorNames.EsEntryPoint);

            AwaitForElastic(actor, injector);
        }

        private async void AwaitForElastic(IActorRef actor, IInjector injector)
        {
            var client = injector.Get<IElasticService>();

            await client.AwaitForConnection();

            actor.Tell(new EsEntryPointActor.ElasticReady());
        }
    }
}
