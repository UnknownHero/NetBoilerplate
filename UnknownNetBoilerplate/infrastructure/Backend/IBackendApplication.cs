using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Backend
{
    public interface IBackendApplication
    {
        string GetId();
        void Run();
    }
}
