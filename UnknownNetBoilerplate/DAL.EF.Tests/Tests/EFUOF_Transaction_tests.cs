using System;
using Bootstrapper;
using Infrastructure.DAL;
using NUnit.Framework;
using Test.Infrastructure.Domain.Fakes;

namespace DAL.EF.Tests.Tests
{
    [TestFixture]
    public class EfuofTransactionTests
    {
        [SetUp]
        public void Init()
        {
            new BootstrapperFactory().GetBoostrapper(BootstrapTypes.IntegrationTest).Run();

            _factory = new EntityFrameworkUnitOfWorkFactory();

            _target = _factory.BeginUnitOfWork();
        }

        private IUnitOfWork _target;
        private EntityFrameworkUnitOfWorkFactory _factory;

        [Test]
        public void Simple_Transaction_Test()
        {
            IUnitOfWork secondUnitOfWork = _factory.BeginUnitOfWork();

            IGenericRepository<FakeEntity, Guid> repo1 = _target.GetRepository<FakeEntity, Guid>();
            IGenericRepository<FakeEntity, Guid> repo2 = secondUnitOfWork.GetRepository<FakeEntity, Guid>();
            Guid repo2Id = Guid.NewGuid();

            repo1.Create(new FakeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Imya0"
                });

            _target.BeginTransaction();
            repo1.Create(new FakeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Imya1"
                });

            
             
            repo2.Create(new FakeEntity
                {
                    Id = repo2Id,
                    Name = "Imya2"
                });

            secondUnitOfWork.Commit();

            Assert.AreEqual(repo2.GetById(repo2Id).Name, "Imya2");
            Assert.AreEqual(_factory.BeginUnitOfWork().GetRepository<FakeEntity, Guid>().Count, 1);

            
            _target.CancelTransaction();
            _target.Commit();


            Assert.AreEqual(repo1.Count, 2);

        }
    }
}