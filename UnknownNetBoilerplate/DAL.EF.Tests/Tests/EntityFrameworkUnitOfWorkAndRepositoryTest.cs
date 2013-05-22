using System;
using System.Data.Common;
using System.Linq;
using DAL.EF.Tests.Fakes;
using Extensions;
using Infrastructure.DAL;
using NUnit.Framework;

namespace DAL.EF.Tests.Tests
{
    [TestFixture]
    public class EntityFrameworkUnitOfWorkAndRepositoryTest
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

            repo.Create(new FakeEntity
                {
                    Id = id,
                    Name = "Hello"
                });

            _target.Commit();

            var item = _target.GetRepository<FakeEntity, Guid>().GetById(id);

            Assert.AreEqual(item.Name, "Hello");

        }

        [Test]
        public void UnitOfWOrk_Repository_Save_GetByAttribute_Test()
        {
            var id = Guid.NewGuid();
            var repo = _target.GetRepository<FakeEntity, Guid>();

            var entity = new FakeEntity
                {
                    Id = id,
                    Name = "Hello"
                };

            repo.Create(entity);

            _target.Commit();

            var item = repo.Get(it => it.Name == entity.Name).FirstOrDefault();

            Assert.AreNotEqual(item, null);
            if (item != null) Assert.AreEqual(item.Name, "Hello");
        }

        [Test]
        public void UnitOfWOrk_Repository_WithoutCommit()
        {
            var id = Guid.NewGuid();
            var repo = _target.GetRepository<FakeEntity, Guid>();

            var entity = new FakeEntity
            {
                Id = id,
                Name = "Hello"
            };

            repo.Create(entity);

//            _target.Commit();

            var item = _target.GetRepository<FakeEntity, Guid>().Get(it => it.Name == entity.Name).FirstOrDefault();

            Assert.AreEqual(item, null);
            
        }

        [Test]
        public void UnitOfWOrk_Repository_Update()
        {
            var id = Guid.NewGuid();
            var repo = _target.GetRepository<FakeEntity, Guid>();

            var entity = new FakeEntity
            {
                Id = id,
                Name = "Hello"
            };

            repo.Create(entity);
            _target.Commit();

            var entity2 = new FakeEntity
            {
                Id = id,
                Name = "UnHello"
            };

            repo.Update(entity2);
            _target.Commit();

            var item = repo.Get(it => it.Id == entity.Id).FirstOrDefault();

            Assert.AreNotEqual(item, null);
            Assert.AreEqual(item.Name, "UnHello");
        }

        [Test]
        public void UnitOfWOrk_Repository_Remove()
        {
            var id = Guid.NewGuid();
            var repo = _target.GetRepository<FakeEntity, Guid>();

            var entity = new FakeEntity
            {
                Id = id,
                Name = "Hello"
            };

            repo.Create(entity);
            _target.Commit();



            repo.Delete(entity);
            _target.Commit();

            var item = repo.Get(it => it.Id == entity.Id).FirstOrDefault();

            Assert.AreEqual(item,null); 
        }

        [Test]
        [Ignore]
        public void UnitOfWOrk_Repository_Transaction()
        {
//            var id = Guid.NewGuid();
//            var repo = _target.GetRepository<FakeEntity, Guid>();
//
//            var entity = new FakeEntity
//            {
//                Id = id,
//                Name = "Hello"
//            };
//
//            var trans = _target.BeginTransaction();
//            
//            repo.Create(entity);
//             
//             
//            _target.Commit();
//
//
// 
//
//            var item = repo.Get(it => it.Id == entity.Id).FirstOrDefault();
//
//            Assert.AreEqual(item, null);
        }


        [Test]
        public void UnitOfWOrk_Repository_Simple_Specification_Test()
        {
            var id = Guid.NewGuid();
            var repo = _target.GetRepository<FakeEntity, Guid>();

            repo.Create(new FakeEntity
            {
                Id = id,
                Name = "Hello"
            });

            _target.Commit();

            var expression = new FakeSpecificationItHelloUser().GetExpression();

            var item = _target.GetRepository<FakeEntity, Guid>().Get(expression).First();

            Assert.AreEqual(item.Name, "Hello");

        }

        [Test]
        public void UnitOfWOrk_Repository_Composite_Specification_Test()
        {
            var id = Guid.NewGuid();
            var repo = _target.GetRepository<FakeEntity, Guid>();

            repo.Create(new FakeEntity
            {
                Id = id,
                Name = "Hello"
            });

            _target.Commit();

           var spec = new FakeSpecificationItHelloUser().And( new FakeSpecificationItUserWithName("Hello") );


           var item = _target.GetRepository<FakeEntity, Guid>().Get(spec.IsSatisfiedBy).FirstOrDefault();

             Assert.AreEqual(item.Name, "Hello");

        }
        
       
    }
}