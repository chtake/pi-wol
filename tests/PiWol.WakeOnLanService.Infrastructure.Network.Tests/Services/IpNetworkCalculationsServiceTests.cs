using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using PiWol.WakeOnLanService.Infrastructure.Network.Services;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Tests.Services
{
    [TestFixture]
    public class IpNetworkCalculationsServiceTests
    {
        [SetUp]
        public void Init()
        {
            _sut = new IpNetworkCalculationsService(NullLoggerFactory.Instance);
        }

        [TearDown]
        public void Dispose()
        {
            _sut = null;
        }

        private IpNetworkCalculationsService _sut;

        [TestCase("10.10.10.100", "255.255.255.0", "10.10.10.255")]
        [TestCase("192.168.1.20", "255.255.255.0", "192.168.1.255")]
        [TestCase("192.168.0.20", "255.255.254.0", "192.168.1.255")]
        [TestCase("192.168.0.20", "255.255.252.0", "192.168.3.255")]
        [TestCase("172.16.0.100", "255.255.0.0", "172.16.255.255")]
        public void TestBroadcastAddress(string ip, string netmask, string expected)
        {
            var actual = _sut.GetBroadcastAddress(ip, netmask);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("255.0.0.0", true)]
        [TestCase("255.255.240.0", true)]
        [TestCase("255.255.255.0", true)]
        [TestCase("255.255.255.254", true)]
        [TestCase("255.255.254.255", false)]
        [TestCase("254.0.0.0", false)]
        public void TestIsValid(string testNetmask, bool expected)
        {
            var actual = _sut.IsSubnetMaskValid(testNetmask);

            Assert.AreEqual(expected, actual);
        }
    }
}