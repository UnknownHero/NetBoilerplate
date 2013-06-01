

using Bootstrapper.Test;
using Infrastructure.Bootstrapper;
using Infrastructure.Root;

namespace Bootstrapper
{
    public class UnitTestsBootstrapper : IBootstrapper
    {
        public void Run()
        {
            var bootstrapper = new UnityTestBootstrapper();  
            ApplicationContainer.Container = bootstrapper.Locator;

        }
    }
}