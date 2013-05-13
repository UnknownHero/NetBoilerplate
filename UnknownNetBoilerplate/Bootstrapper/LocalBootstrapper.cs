using Bootstrapper.Local;
using Infrastructure.Bootstrapper;
using Infrastructure.Root;
using Microsoft.Practices.ServiceLocation;

namespace Bootstrapper
{
    public class LocalBootstrapper : IBootstrapper
    {
        public void Run()
        {
            var bootstrapper = new UnityLocalBootstrapper();  
            ApplicationContainer.Container = bootstrapper.Locator;

        }
    }
}