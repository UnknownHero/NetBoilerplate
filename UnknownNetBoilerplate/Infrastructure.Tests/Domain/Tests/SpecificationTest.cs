 
using System;
using Infrastructure.Tests.Domain.Fakes;
using NUnit.Framework;
using Extensions;

namespace Infrastructure.Tests.Domain.Tests
{
    [TestFixture]
    public class SpecificationTest
    {
        private FakeSpecificationIsUra _uraSpec;
        private FakeSpecificationIsMisha _mishaSpec;
        private FakeSpecificationIsSasha _sahsaSpec;

        [SetUp]
        public void Init()
        {
            _uraSpec = new FakeSpecificationIsUra();
            _mishaSpec = new FakeSpecificationIsMisha();
            _sahsaSpec = new FakeSpecificationIsSasha();
        }

 

        [Test]
        public void Test_One_Specification()
        {
            var entity = new FakeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = FakeNames.Ura
                };

            var trueResult = _uraSpec.IsSatisfiedBy(entity);
            entity.Name = FakeNames.Misha;
            var falseResult1 = _uraSpec.IsSatisfiedBy(entity);
            entity.Name = FakeNames.Sasha;
            var falseResult2 = _uraSpec.IsSatisfiedBy(entity);

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

            

            Assert.AreEqual(true, _uraSpec.And(_sahsaSpec.Not()).IsSatisfiedBy(entity)  );

            Assert.AreEqual(false, _sahsaSpec.Or(_mishaSpec).IsSatisfiedBy(entity) );

            Assert.AreEqual(false, _sahsaSpec.Or(_uraSpec.Not()).IsSatisfiedBy(entity) );

            Assert.AreEqual(true, _sahsaSpec.Or(_uraSpec).IsSatisfiedBy(entity) ); 
        }
    }

    
}
