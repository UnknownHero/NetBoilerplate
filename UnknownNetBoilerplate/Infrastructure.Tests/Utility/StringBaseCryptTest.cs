using System;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Utility;
using NUnit.Framework;

namespace Infrastructure.Tests.Utility
{
    [TestFixture]
    public class StringBaseCryptTest
    {
        private StringBaseCrypt _target;

        [SetUp]
        public void Init()
        {

            _target = new StringBaseCrypt();
        }

        [Test]
        public void Real_Encryption_Test()
        {

            var value = "HelloTest";
            var enc = _target.Encrypt(value);
            var dec = _target.Decrypt(enc);

            Assert.AreEqual(dec , value);
            Assert.AreNotEqual(enc , dec);
            Assert.AreNotEqual(enc, value);
        }
    }

}