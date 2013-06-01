

using Bootstrapper.Integration;
 
using Infrastructure.Bootstrapper;
using Infrastructure.Root;

namespace Bootstrapper
{
    public class IntegrationBootstrapper : IBootstrapper
    {
        public void Run()
        {
            var bootstrapper = new UnityIntegrationsBootstrapper();  
            ApplicationContainer.Container = bootstrapper.Locator;

        }
    }
}