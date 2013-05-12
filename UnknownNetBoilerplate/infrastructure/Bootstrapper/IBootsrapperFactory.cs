using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Bootstrapper;

namespace Infrastructure.Bootstrapper
{
    public interface IBootsrapperFactory<TIndex>
    {
        IBootstrapper GetBoostrapper(TIndex index);
    }
}
