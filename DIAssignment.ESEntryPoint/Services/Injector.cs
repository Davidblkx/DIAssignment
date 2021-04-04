using DIAssignment.Core.Services;

namespace DIAssignment.ESEntryPoint.Services
{
    public class Injector : BaseInjector
    {
        protected override void RegisterServices()
        {
            Container.Register<IElasticService, ElasticService>();
        }
    }
}
