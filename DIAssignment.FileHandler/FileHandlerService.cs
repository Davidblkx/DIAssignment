using DIAssignment.Core.Actors;

namespace DIAssignment.FileHandler
{
    internal sealed class FileHandlerService : BaseActorService
    {
        public static FileHandlerService Start() => new();
    }
}
