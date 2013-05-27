using DAL.EF;
using Infrastructure.Bootstrapper;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Bootstrapper.Test
{
    public class UnityTestBootstrapper : CommonBootstrapper
    {
        protected override UnityServiceLocator CreateServiceLocator()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return new UnityServiceLocator(container);
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDbContextGenerator, TestDbContextGenerator>();
        }
    }
}
