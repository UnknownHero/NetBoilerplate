 
using System;
using Bootstrapper;
using NUnit.Framework;
using Test.Infrastructure.Domain.Fakes;


namespace Infrastructure.Tests.Domain.Tests
{
    [TestFixture]
    public class SpecificationTest
    {
        private UraSpec _uraSpec;
        private MishaSpec _mishaSpec;
        private SashaSpec _sahsaSpec;

        [SetUp]
        public void Init()
        {

            new BootstrapperFactory().GetBoostrapper(BootstrapTypes.UnitTest).Run();

            _uraSpec = new UraSpec();
            _mishaSpec = new MishaSpec();
            _sahsaSpec = new SashaSpec();
        }

 

        [Test]
        public void Test_One_Specification()
        {
            var entity = new FakeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = FakeNames.Ura
                };

            var trueResult = _uraSpec.IsSatisfied(entity);
            entity.Name = FakeNames.Misha;
            var falseResult1 = _uraSpec.IsSatisfied(entity);
            entity.Name = FakeNames.Sasha;
            var falseResult2 = _uraSpec.IsSatisfied(entity);

            Assert.AreEqual(true, trueResult);
            Assert.AreEqual(false, falseResult1);
            Assert.AreEqual(false, falseResult2);

            
        }

        [Test]
        public void Test_Composite_Specification()
        {
            var entity = new FakeEntity
            {
                Id = Guid.NewGuid(),
                Name = FakeNames.Ura
            };



            Assert.AreEqual(true, _uraSpec.And(_sahsaSpec).Negated(_mishaSpec).IsSatisfied(entity));

            Assert.AreEqual(false, _sahsaSpec.Or(_mishaSpec).IsSatisfied(entity));

            Assert.AreEqual(false, _sahsaSpec.Or(_uraSpec).Negated(_mishaSpec).IsSatisfied(entity));

            Assert.AreEqual(true, _sahsaSpec.Or(_mishaSpec).Negated(_uraSpec).IsSatisfied(entity)); 
        }

        [Test]
        public void Test_Composite_NSpecification()
        {
            var entity = new FakeEntity
            {
                Id = Guid.NewGuid(),
                Name = FakeNames.Ura
            };
             
            var allSpec = _uraSpec.Or(_sahsaSpec).And(_mishaSpec).Negated(_mishaSpec);

            Assert.AreEqual(true, allSpec.IsSatisfied(entity));
             
        }
    }

    
}
