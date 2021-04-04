using Akka.Actor;
using DIAssignment.Core.Actors;
using DIAssignment.Core.Infra;
using DIAssignment.Projection.Actors;
using DIAssignment.Projection.Services;

namespace DIAssignment.Projection
{
    internal sealed class ProjectionService : BaseActorService
    {
        public static ProjectionService Start() => new();

        protected override void OnStart()
        {
            var injector = new Injector();

            ActorSystem.ActorOf(
                Props.Create<ProjectionMaster>(injector),
                ActorNames.Projection
            );
        }
    }
}
