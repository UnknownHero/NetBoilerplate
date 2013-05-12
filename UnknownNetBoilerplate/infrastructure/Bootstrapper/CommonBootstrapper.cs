using Microsoft.Practices.ServiceLocation;

namespace Infrastructure.Bootstrapper
{
    public abstract class CommonBootstrapper
    {
        public static IServiceLocator Locator;

        protected CommonBootstrapper()
        {
            Locator = CreateServiceLocator();
        }

        protected abstract IServiceLocator CreateServiceLocator();
    } 
}