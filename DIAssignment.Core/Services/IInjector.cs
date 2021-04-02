namespace DIAssignment.Core.Services
{
    public interface IInjector
    {
        public T Get<T>() where T : class;
    }
}
