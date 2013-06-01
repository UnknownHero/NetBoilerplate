using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.OleDb;
using DAL.EF;
using Test.Infrastructure.DAL;

namespace Bootstrapper.Integration
{
    public class IntegrationDbContextGenerator: IDbContextGenerator
    {
        public DbContext GetContext()
        { 
            var connection = "Data Source=.\\sqlexpress;Initial Catalog=IntegrationTest;Integrated Security=SSPI;MultipleActiveResultSets=true;";
            var context = new IntegrationDbContext(connection); 
             
            context.Database.CreateIfNotExists();
            context.Database.Connection.Open();
            return context;
        }
    
    }
}
