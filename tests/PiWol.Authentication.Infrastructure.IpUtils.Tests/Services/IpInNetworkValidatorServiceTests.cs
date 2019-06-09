using System;
using NUnit.Framework;
using PiWol.Authentication.Infrastructure.IpUtils.Services;

namespace PiWol.Authentication.Infrastructure.IpUtils.Tests.Services
{
    [TestFixture]
    public class IpInNetworkValidatorServiceTests
    {
        [SetUp]
        public void Init()
        {
            _sut = new IpInNetworkValidatorService();
        }

        [TearDown]
        public void Dispose()
        {
            _sut = null;
        }

        private IpInNetworkValidatorService _sut;

        [TestCase("10.10.10.100", "10.10.10.10/24", true)]
        [TestCase("10.10.10.100", "10.10.10.100", true)]
        [TestCase("10.10.10.100", "10.10.10.100/32", true)]
        [TestCase("10.10.10.100", "10.10.10.10/28", false)]
        public void TestIsInRange(string ip, string cidr, bool expected)
        {
            var actual = _sut.IsInRange(ip, cidr);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidCidrNetwork()
        {
            Assert.Throws<FormatException>(() => _sut.IsInRange("192.168.2.1", "10.10.10.0/33"));
            Assert.Throws<FormatException>(() => _sut.IsInRange("192.168.2.1", "10.10.260.10"));
        }

        [Test]
        public void InvalidIpAddress()
        {
            Assert.Throws<FormatException>(() => _sut.IsInRange("265.12.10.2", "10.10.10.0/24"));
        }
    }
}