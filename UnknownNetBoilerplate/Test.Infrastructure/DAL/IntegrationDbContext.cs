using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using Test.Infrastructure.Domain.Fakes;

namespace Test.Infrastructure.DAL
{
    public class IntegrationDbContext: DatabaseMap
    {
        public IntegrationDbContext() : base()
        { 

        }

        public IntegrationDbContext(DbConnection connection)
            : base(connection, true)
        {
 
        }

        public IntegrationDbContext(string dbConnection)
            : base(dbConnection)
      {
          
      }

        public IDbSet<FakeEntity> FakeEntity { get; set; }

    

		public void Dispose()
		{
			// Do nothing!
		}
    }
}
