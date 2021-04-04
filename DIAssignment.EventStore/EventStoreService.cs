using Akka.Actor;
using Akka.Routing;
using DIAssignment.Core.Actors;
using DIAssignment.Core.Infra;
using DIAssignment.EventStore.Actors;
using DIAssignment.EventStore.Services;

namespace DIAssignment.EventStore
{
    internal sealed class EventStoreService : BaseActorService
    {
        public static EventStoreService Start() => new();

        protected override void OnStart()
        {
            var injector = new Injector();

            ActorSystem.ActorOf(
                Props.Create<SerializerActor>(injector)
                .WithRouter(new RoundRobinPool(50)), ActorNames.Serializer);
        }
    }
}
