 

using System;
using System.Data.Common;
using System.Data.Entity;
using System.Reflection;
using Test.Infrastructure.Domain.Fakes;

namespace Test.Infrastructure.DAL
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext()
        {
           
            // Set up your collections
            FakeEntity = new InMemoryDbSet<FakeEntity>();

        }

        public InMemoryDbContext(DbConnection connection)
            : base(connection, true)
        {
            FakeEntity = new InMemoryDbSet<FakeEntity>();
        }

        public IDbSet<FakeEntity> FakeEntity { get; set; }

        public IDbSet<T> Set<T>() where T : class
        {
            foreach (PropertyInfo property in typeof(InMemoryDbContext).GetProperties())
            {
                if (property.PropertyType == typeof (IDbSet<T>))
                    return property.GetValue(this, null) as IDbSet<T>;
            }
            throw new Exception("Type collection not found");
        }

		public void Dispose()
		{
			// Do nothing!
		}
    }
}