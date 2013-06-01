using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace DAL.EF
{
    public class DatabaseMap : DbContext
    {
        public DatabaseMap()
            : base()
        {
            
        }

      public DatabaseMap(DbConnection connection ,Boolean boolVar)
            : base(connection, boolVar)
      {
          
      }
      
      public DatabaseMap(string dbConnection) : base(dbConnection)
      {
          
      }

    }
}
