using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using Infrastructure.Bootstrapper;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;


namespace Bootstrapper.Local
{
    public class UnityLocalBootstrapper : CommonBootstrapper
    {
        protected override UnityServiceLocator CreateServiceLocator()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return new UnityServiceLocator(container);
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDbContextGenerator, LocalDbContextGenerator>();
        }
    }
}
