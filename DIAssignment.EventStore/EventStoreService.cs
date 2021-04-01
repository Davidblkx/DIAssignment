using DIAssignment.Core.Actors;

namespace DIAssignment.EventStore
{
    internal sealed class EventStoreService : BaseActorService
    {
        public static EventStoreService Start() => new();
    }
}
