using DIAssignment.Core.Services;
using SimpleInjector;

namespace DIAssignment.FileHandler.Services
{
    /// <summary>
    /// Manage dependencies
    /// </summary>
    internal sealed class Injector : IInjector
    {
        public Container Container { get; }

        public Injector()
        {
            Container = new Container();
            RegisterServices();
        }

        public T Get<T>() where T : class
            => Container.GetInstance<T>();

        private void RegisterServices()
        {
            Container.Register<IConfigService, ConfigService>();
            Container.Register<IGDriveService, GDriveService>();
            Container.Register<IZipService, ZipService>();
            Container.Register<IDbFileReaderService, DbFileReaderService>();

            Container.Verify();
        }
    }
}
