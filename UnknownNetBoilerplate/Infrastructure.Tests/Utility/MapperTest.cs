using EmitMapper;
using Infrastructure.Utility;
using NUnit.Framework;

namespace Infrastructure.Tests.Utility
{
    [TestFixture]
    public class MapperTest
    {
        [SetUp]
        public void Init()
        {
            _target = Mapper.Instance;
        }

        private ObjectMapperManager _target;

        [Test]
        public void True_Test()
        {
            //Simple Emitmapper wrapper without tests
            Assert.AreEqual(true, true);
        }
    }
}