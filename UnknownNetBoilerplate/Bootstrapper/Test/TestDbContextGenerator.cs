using System.Data.Entity;
using DAL.EF;
using Test.Infrastructure.DAL;

namespace Bootstrapper.Test
{
    public class TestDbContextGenerator : IDbContextGenerator
    {
        public DbContext GetContext()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient(); 
            return new InMemoryDbContext(connection);
        }
    }
}
