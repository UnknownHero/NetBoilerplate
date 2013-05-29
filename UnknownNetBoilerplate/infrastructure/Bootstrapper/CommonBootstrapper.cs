using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Infrastructure.Bootstrapper
{
    public abstract class CommonBootstrapper
    {
        public UnityServiceLocator Locator;

        protected CommonBootstrapper()
        {
            Locator = CreateServiceLocator();
        }

        protected abstract UnityServiceLocator CreateServiceLocator();
    } 
}