 

using Bootstrapper;

namespace DAL.EF.Tests
{
    public class TestBootstrapper
    {
        public void Run()
        {
            new BootstrapperFactory().GetBoostrapper(BootstrapTypes.Test).Run();
        }

    }
}
