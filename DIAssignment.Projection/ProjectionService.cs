using DIAssignment.Core.Actors;

namespace DIAssignment.Projection
{
    internal sealed class ProjectionService : BaseActorService
    {
        public static ProjectionService Start() => new();
    }
}
