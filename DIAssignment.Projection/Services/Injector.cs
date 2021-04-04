using DIAssignment.Core.Services;

namespace DIAssignment.Projection.Services
{
    public class Injector : BaseInjector
    {
        protected override void RegisterServices()
        {
            Container.Register<IMongoService, MongoService>();
        }
    }
}
