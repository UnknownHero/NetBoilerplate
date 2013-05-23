using Bootstrapper.Local;
using Bootstrapper.Test;
using Infrastructure.Bootstrapper;
using Infrastructure.Root;
using Microsoft.Practices.ServiceLocation;

namespace Bootstrapper
{
    public class TestBootstrapper : IBootstrapper
    {
        public void Run()
        {
            var bootstrapper = new UnityTestBootstrapper();  
            ApplicationContainer.Container = bootstrapper.Locator;

        }
    }
}