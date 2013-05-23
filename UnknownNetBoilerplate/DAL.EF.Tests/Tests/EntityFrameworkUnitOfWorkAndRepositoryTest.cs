using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using DAL.EF.Tests.Fakes; 
using Infrastructure.DAL;
using Infrastructure.Tests.Domain.Fakes;
using NUnit.Framework;
 

namespace DAL.EF.Tests.Tests
{

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

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
                Name = FakeNames.Ura
            });

            _target.Commit();

            repo.Create(new FakeEntity
            {
                Id = Guid.NewGuid(),
                Name = FakeNames.Misha
            });

            _target.Commit();


            var expression = new UraSpec().is_satisfied_by();

            var items = _target.GetRepository<FakeEntity, Guid>().Get(expression);

            Assert.AreEqual(items.Count(), 1);
            Assert.AreEqual(items.First().Name, FakeNames.Ura);

        }

        [Test]
        public void UnitOfWOrk_Repository_Composite_Specification_Test()
        {
            var id = Guid.NewGuid();
            var repo = _target.GetRepository<FakeEntity, Guid>();

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


            var expression = new UraSpec().And(new MishaSpec().Not()).is_satisfied_by();

            var items = _target.GetRepository<FakeEntity, Guid>().Get(expression);

            Assert.AreEqual(items.Count(), 1);
            Assert.AreEqual(items.First().Name, FakeNames.Ura);

        }
        
       
    }
}