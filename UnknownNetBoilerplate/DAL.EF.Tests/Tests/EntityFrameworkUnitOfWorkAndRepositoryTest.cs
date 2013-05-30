using System;
using System.Linq;
using System.Linq.Expressions;
using Bootstrapper;
using Infrastructure.DAL;
using Infrastructure.Domain.Specification;
using NUnit.Framework;
using Test.Infrastructure.Domain.Fakes;

namespace DAL.EF.Tests.Tests
{
    [TestFixture]
    public class EntityFrameworkUnitOfWorkAndRepositoryTest
    {
        [SetUp]
        public void Init()
        {
            new  BootstrapperFactory().GetBoostrapper(BootstrapTypes.Test).Run();

            _factory = new EntityFrameworkUnitOfWorkFactory();

            _target = _factory.BeginUnitOfWork();
        }

        private IUnitOfWork _target;
        private EntityFrameworkUnitOfWorkFactory _factory;

        [Test]
        public void UnitOfWOrk_Repository_Composite_Specification_Test()
        {
            Guid id = Guid.NewGuid();
            IGenericRepository<FakeEntity, Guid> repo = _target.GetRepository<FakeEntity, Guid>();

            repo.Create(new FakeEntity
                {
                    Id = id,
                    Name = FakeNames.Ura
                });

            _target.Commit();

            repo.Create(new FakeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = FakeNames.Misha
                });

            _target.Commit();

            var uraSpec = new UraSpec();
            var mishaSpec = new MishaSpec();
            var sashaSpec = new SashaSpec();

            Expression<Func<FakeEntity, bool>> expression = uraSpec.And(mishaSpec).Negated(sashaSpec).IsSatisfied();

            IQueryable<FakeEntity> items = _target.GetRepository<FakeEntity, Guid>().Get(expression);

            Assert.AreEqual(items.Count(), 2);

            FakeEntity uraFromDb =
                _target.GetRepository<FakeEntity, Guid>()
                       .Get(new LambdaSpecification<FakeEntity>(it => it.Id == id).IsSatisfied())
                       .First();

            Assert.AreEqual(uraFromDb.Name, FakeNames.Ura);
        }

        [Test]
        public void UnitOfWOrk_Repository_Remove()
        {
            Guid id = Guid.NewGuid();
            IGenericRepository<FakeEntity, Guid> repo = _target.GetRepository<FakeEntity, Guid>();

            var entity = new FakeEntity
                {
                    Id = id,
                    Name = "Hello"
                };

            repo.Create(entity);
            _target.Commit();


            repo.Delete(entity);
            _target.Commit();

            FakeEntity item = repo.Get(it => it.Id == entity.Id).FirstOrDefault();

            Assert.AreEqual(item, null);
        }

        [Test]
        public void UnitOfWOrk_Repository_Save_GetByAttribute_Test()
        {
            Guid id = Guid.NewGuid();
            IGenericRepository<FakeEntity, Guid> repo = _target.GetRepository<FakeEntity, Guid>();

            var entity = new FakeEntity
                {
                    Id = id,
                    Name = "Hello"
                };

            repo.Create(entity);

            _target.Commit();

            FakeEntity item = repo.Get(it => it.Name == entity.Name).FirstOrDefault();

            Assert.AreNotEqual(item, null);
            if (item != null) Assert.AreEqual(item.Name, "Hello");
        }

        [Test]
        public void UnitOfWOrk_Repository_Save_GetById_Test()
        {
            Guid id = Guid.NewGuid();
            IGenericRepository<FakeEntity, Guid> repo = _target.GetRepository<FakeEntity, Guid>();

            repo.Create(new FakeEntity
                {
                    Id = id,
                    Name = "Hello"
                });

            _target.Commit();

            FakeEntity item = _target.GetRepository<FakeEntity, Guid>().GetById(id);

            Assert.AreEqual(item.Name, "Hello");
        }


        [Test]
        public void UnitOfWOrk_Repository_Simple_Specification_Test()
        {
            Guid id = Guid.NewGuid();
            IGenericRepository<FakeEntity, Guid> repo = _target.GetRepository<FakeEntity, Guid>();

            repo.Create(new FakeEntity
                {
                    Id = id,
                    Name = FakeNames.Ura
                });

            _target.Commit();

            repo.Create(new FakeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = FakeNames.Misha
                });

            _target.Commit();


            Expression<Func<FakeEntity, bool>> expression = new UraSpec().IsSatisfied();

            IQueryable<FakeEntity> items = _target.GetRepository<FakeEntity, Guid>().Get(expression);

            Assert.AreEqual(items.Count(), 1);
            Assert.AreEqual(items.First().Name, FakeNames.Ura);
        }

        [Test]
        public void UnitOfWOrk_Repository_Transaction()
        {
            Guid id = Guid.NewGuid();
            IGenericRepository<FakeEntity, Guid> repo = _target.GetRepository<FakeEntity, Guid>();

            var entity = new FakeEntity
                {
                    Id = id,
                    Name = "Hello"
                };

            IUnitOfWork transUoW = _factory.BeginUnitOfWork();
            IGenericRepository<FakeEntity, Guid> transRepo = transUoW.GetRepository<FakeEntity, Guid>();

            transRepo.Create(entity);


            FakeEntity nullEntity = _target.GetRepository<FakeEntity, Guid>().GetById(id);
            FakeEntity NotNullEntity = transRepo.GetById(id);


            Assert.AreEqual(nullEntity, null);
            Assert.AreEqual(NotNullEntity.Name, entity.Name);
             
        }

        [Test]
        public void UnitOfWOrk_Repository_Update()
        {
            Guid id = Guid.NewGuid();
            IGenericRepository<FakeEntity, Guid> repo = _target.GetRepository<FakeEntity, Guid>();

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

            FakeEntity item = repo.Get(it => it.Id == entity.Id).FirstOrDefault();

            Assert.AreNotEqual(item, null);
            Assert.AreEqual(item.Name, "UnHello");
        }

        [Test]
        public void UnitOfWOrk_Repository_WithoutCommit()
        {
            Guid id = Guid.NewGuid();
            IGenericRepository<FakeEntity, Guid> repo = _target.GetRepository<FakeEntity, Guid>();

            var entity = new FakeEntity
                {
                    Id = id,
                    Name = "Hello"
                };

            repo.Create(entity);

//            _target.Commit();

            FakeEntity item =
                _target.GetRepository<FakeEntity, Guid>().Get(it => it.Name == entity.Name).FirstOrDefault();

            Assert.AreEqual(item, null);
        }
    }
}