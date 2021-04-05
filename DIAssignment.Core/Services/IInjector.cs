namespace DIAssignment.Core.Services
{
    /// <summary>
    /// Dependency injector service to handle dependencies
    /// </summary>
    public interface IInjector
    {
        public T Get<T>() where T : class;
    }
}
