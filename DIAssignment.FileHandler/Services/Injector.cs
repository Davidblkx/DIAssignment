using DIAssignment.Core.Services;
using SimpleInjector;

namespace DIAssignment.FileHandler.Services
{
    /// <summary>
    /// Manage dependencies
    /// </summary>
    internal sealed class Injector : BaseInjector
    {
        protected override void RegisterServices()
        {
            Container.Register<IGDriveService, GDriveService>();
            Container.Register<IZipService, ZipService>();
            Container.Register<IDbFileReaderService, DbFileReaderService>();
        }
    }
}
