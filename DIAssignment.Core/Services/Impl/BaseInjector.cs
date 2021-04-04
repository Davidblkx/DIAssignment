using SimpleInjector;

namespace DIAssignment.Core.Services
{
    public abstract class BaseInjector : IInjector
    {
        public Container Container { get; }

        public BaseInjector()
        {
            Container = new Container();

            Container.Register<IConfigService, ConfigService>();
            RegisterServices();

            Container.Verify();
        }

        public T Get<T>() where T : class
            => Container.GetInstance<T>();

        protected abstract void RegisterServices();
    }
}
