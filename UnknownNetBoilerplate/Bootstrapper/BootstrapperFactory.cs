using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Bootstrapper;

namespace Bootstrapper
{
    public class BootstrapperFactory : IBootsrapperFactory<BootstrapTypes>
    {
        public IBootstrapper GetBoostrapper(BootstrapTypes index)
        {
            if (index == BootstrapTypes.Local)
            {
                return new LocalBootstrapper();
            }
            else if (index == BootstrapTypes.UnitTest)
            {
                return new UnitTestsBootstrapper();
            }
            else if (index == BootstrapTypes.IntegrationTest)
            {
                return new IntegrationBootstrapper();
            }
            else
            {
                throw new Exception("Unknown bootstrap type: " + index);
            }
        }
    }
}
