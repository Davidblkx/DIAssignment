using Akka.Actor;

namespace DIAssignment.FileHandler.Actors
{
    public static class ActorRef
    {
        public static IActorRef FileHandler { get; set; } = ActorRefs.Nobody;
    }
}
