using System;
using System.Data.Entity;
using DAL.EF;

namespace Bootstrapper.Local
{
    public class LocalDbContextGenerator: IDbContextGenerator
    {
        public DbContext GetContext()
        {
            throw new NotImplementedException();
        }
    }
}