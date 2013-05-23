using System;
using System.Data.Entity;

namespace DAL.EF
{
    public class LocalDbContextGenerator: IDbContextGenerator
    {
        public DbContext GetContext()
        {
           throw new NotImplementedException();
        }
    }
}