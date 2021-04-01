using DIAssignment.Core.Actors;

namespace DIAssignment.ESEntryPoint
{
    internal sealed class ESEntryPointService : BaseActorService
    {
        public static ESEntryPointService Start() => new();
    }
}
