namespace DIAssignment.Core.Infra
{
    /// <summary>
    /// Actor names to be shared across services
    /// </summary>
    public class ActorNames
    {
        public const string FileHandler = "fileimport";
        public const string Serializer = "serializer";
        public const string Projection = "projection";
        public const string EsEntryPoint = "esentrypoint";
    }
}
