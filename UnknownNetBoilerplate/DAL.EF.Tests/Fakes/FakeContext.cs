using System;
using System.Data.Common;
using System.Data.Entity;
using System.Reflection;

namespace DAL.EF.Tests.Fakes
{
    public class FakeContext : DbContext
    {
        public FakeContext()
        {
           
            // Set up your collections
            FakeEntity = new FakeDbSet<FakeEntity>
                {
                    new FakeEntity {Name = "Brent"}
                };
            
        }

        public FakeContext(DbConnection connection) : base(connection,true)
        {
            
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

        public void SaveChanges()
        {
            // do nothing (probably set a variable as saved for testing)
        }
    }
}