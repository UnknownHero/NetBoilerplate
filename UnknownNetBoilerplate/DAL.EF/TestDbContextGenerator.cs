using System.Data.Entity;
using Test.Infrastructure.DAL;


namespace DAL.EF
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
