using System;
using System.Data.Common;
using DAL.EF.Tests.Fakes;
using Infrastructure.DAL;
using NUnit.Framework;

namespace DAL.EF.Tests.Tests
{
    [TestFixture]
    public class EntityFrameworkUnitOfWorkTest
    {
        [SetUp]
        public void Init()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();

            var factory = new EntityFrameworkUnitOfWorkFactory(new FakeContext(connection));
     
            _target = factory.BeginUnitOfWork();
        }

        private IUnitOfWork _target;

        [Test]
        public void UnitOfWOrk_Repository_Save_GetById_Test()
        {
            var id = Guid.NewGuid();
            var repo = _target.GetRepository<FakeEntity, Guid>();

            repo.Insert(new FakeEntity
                {
                    Id = id,
                    Name = "Hello"
                });

            _target.Commit();

            var item = _target.GetRepository<FakeEntity, Guid>().GetById(id);

            Assert.AreEqual(item.Name, "Hello");

        }
    }
}