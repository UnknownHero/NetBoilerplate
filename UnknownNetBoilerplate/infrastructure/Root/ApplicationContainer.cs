using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace Infrastructure.Root
{
    public class ApplicationContainer
    {
        /// <summary>
        /// Global container for the whole project (without end customer/UI/Console)
        /// </summary>
         public static IServiceLocator Container = null;
    }
}
