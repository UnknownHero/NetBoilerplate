using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Bootstrapper;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Bootstrapper.Local
{
    public class UnityLocalBootstrapper : CommonBootstrapper
    {
        protected override IServiceLocator CreateServiceLocator()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return new UnityServiceLocator(container);
        }

        private static void RegisterTypes(IUnityContainer container)
        {
//            container.RegisterType<ICustomer, GoldenCustomer>(typeof(ICustomer).FullName);
        }
    }
}
