using System.Net;
using NetTools;
using PiWol.Authentication.Abstraction.Services;

namespace PiWol.Authentication.Infrastructure.IpUtils.Services
{
    internal class IpInNetworkValidatorService : IIpInNetworkValidatorService
    {
        public bool IsInRange(string ipAddress, string cidrNetwork)
        {
            return IsInRange(IPAddress.Parse(ipAddress), cidrNetwork);
        }

        public bool IsInRange(IPAddress ipAddress, string cidrNetwork)
        {
            var range = IPAddressRange.Parse(cidrNetwork);

            return range.Contains(ipAddress);
        }

        public string ParseRange(string cidrNetwork)
        {
            var range = IPAddressRange.Parse(cidrNetwork);
            return range.ToCidrString();
        }
    }
}