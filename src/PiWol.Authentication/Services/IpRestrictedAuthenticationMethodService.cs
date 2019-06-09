using System.Linq;
using Microsoft.AspNetCore.Http;
using PiWol.Authentication.Abstraction.Services;
using PiWol.Authentication.Data.Abstraction.Repositories;

namespace PiWol.Authentication.Services
{
    internal class IpRestrictedAuthenticationMethodService : IAuthenticationMethodService
    {
        private readonly IHttpContextAccessor _context;
        private readonly IIpNetworkRepository _repository;
        private readonly IIpInNetworkValidatorService _validatorService;

        public IpRestrictedAuthenticationMethodService(
            IIpNetworkRepository repository,
            IIpInNetworkValidatorService validatorService,
            IHttpContextAccessor context)
        {
            _repository = repository;
            _validatorService = validatorService;
            _context = context;
        }

        public bool? TryAccessGranted()
        {
            var ipList = _repository.ReadAll().Select(x => x.IpNetwork).ToList();
            if (ipList.Any() == false)
            {
                return null;
            }

            var requestIp = _context.HttpContext.Connection.RemoteIpAddress.MapToIPv4();

            foreach (var cidr in ipList)
            {
                if (_validatorService.IsInRange(requestIp, cidr))
                {
                    return true;
                }
            }

            return false;
        }
    }
}