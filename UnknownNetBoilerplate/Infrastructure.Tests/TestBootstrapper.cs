using Bootstrapper;

namespace Infrastructure.Tests
{
    public class TestBootstrapper
    {
        public void Run()
        {
            new BootstrapperFactory().GetBoostrapper(BootstrapTypes.UnitTest).Run();
        } 
    }
}