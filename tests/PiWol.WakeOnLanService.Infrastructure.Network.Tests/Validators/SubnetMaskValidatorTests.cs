using FakeItEasy;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Abstraction.Validators;
using PiWol.WakeOnLanService.Infrastructure.Network.Validators;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Tests.Validators
{
    [TestFixture]
    public class SubnetMaskValidatorTests
    {
        [SetUp]
        public void Init()
        {
            _networkCalcService = A.Fake<IIpNetworkCalculationsService>();
            _sut = new SubnetMaskValidator(NullLoggerFactory.Instance, _networkCalcService);
        }

        [TearDown]
        public void Dispose()
        {
            _sut = null;
        }

        [Test]
        public void TestIsValid()
        {
            A.CallTo(() => _networkCalcService.IsSubnetMaskValid(A<string>.Ignored)).Returns(true);
            var actual = _sut.IsValid("255.255.255.0");
            var expected = true;
            Assert.AreEqual(expected, actual);
            A.CallTo(() => _networkCalcService.IsSubnetMaskValid(A<string>.Ignored)).MustHaveHappened();
        }

        private ISubnetMaskValidator _sut;

        private IIpNetworkCalculationsService _networkCalcService;
    }
}