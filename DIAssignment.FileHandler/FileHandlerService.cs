using Akka.Actor;
using Akka.Routing;
using DIAssignment.Core.Actors;
using DIAssignment.Core.Infra;
using DIAssignment.FileHandler.Actors;
using DIAssignment.FileHandler.Services;

namespace DIAssignment.Core
{
    internal sealed class FileHandlerService : BaseActorService
    {
        public static FileHandlerService Start() => new();

        protected override void OnStart()
        {
            var injector = new Injector();

            ActorRef.FileHandler = ActorSystem.ActorOf(
                Props.Create<FileHandlerActor>(injector)
                .WithRouter(FromConfig.Instance), ActorNames.FileHandler);
        }
    }
}
