using Microsoft.Extensions.Logging;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Abstraction.Validators;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Validators
{
    public class SubnetMaskValidator : ISubnetMaskValidator
    {
        private readonly ILogger _logger;

        private readonly IIpNetworkCalculationsService _service;

        public SubnetMaskValidator(ILoggerFactory loggerFactory, IIpNetworkCalculationsService service)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public bool IsValid(string netmask)
        {
            return _service.IsSubnetMaskValid(netmask);
        }
    }
}