using Bootstrapper;
using Infrastructure.Bootstrapper;
using Infrastructure.Root;
using NUnit.Framework;

namespace Infrastructure.Tests.Utility
{
    [TestFixture]
    public class BootsrapperFactoryTest
    {
        [SetUp]
        public void Init()
        {
            _target = new BootstrapperFactory();
        }

        private BootstrapperFactory _target;


        [Test]
        public void Local_Bootstrapper_Test()
        {
            IBootstrapper bootstrapper = _target.GetBoostrapper(BootstrapTypes.Local);

            Assert.AreEqual(bootstrapper.GetType().Name, typeof (LocalBootstrapper).Name);
        }

        [Test]
        public void Local_Bootstrapper_Infrastructure_Link_Test()
        {
            IBootstrapper bootstrapper = _target.GetBoostrapper(BootstrapTypes.Local);
            bootstrapper.Run();

            Assert.AreNotEqual(ApplicationContainer.Container, null );
        }
    }
}