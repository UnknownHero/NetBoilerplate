using System;
using System.Data.Common;
using System.Data.Entity;
using System.Reflection;
using FakeDbSet;
using Infrastructure.Tests.Domain.Fakes;

namespace DAL.EF.Tests.Fakes
{
    public class FakeContext : DbContext
    {
        public FakeContext()
        {
           
            // Set up your collections
            FakeEntity = new FakeDbSet<FakeEntity>();

        }

        public FakeContext(DbConnection connection) : base(connection,true)
        {
            FakeEntity = new FakeDbSet<FakeEntity>();
        }

        public IDbSet<FakeEntity> FakeEntity { get; set; }

        public IDbSet<T> Set<T>() where T : class
        {
            foreach (PropertyInfo property in typeof(FakeContext).GetProperties())
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