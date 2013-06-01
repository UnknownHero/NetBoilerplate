using Bootstrapper.Test;
using DAL.EF;
using Infrastructure.Bootstrapper;
using Microsoft.Practices.Unity;

namespace Bootstrapper.Integration
{
    public class UnityIntegrationsBootstrapper : CommonBootstrapper
    {
        protected override UnityServiceLocator CreateServiceLocator()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return new UnityServiceLocator(container);
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDbContextGenerator, IntegrationDbContextGenerator>();
        }
    }
}
