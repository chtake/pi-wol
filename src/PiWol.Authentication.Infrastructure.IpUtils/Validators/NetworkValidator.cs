using NetTools;
using PiWol.Authentication.Abstraction.Validators;

namespace PiWol.Authentication.Infrastructure.IpUtils.Validators
{
    internal class NetworkValidator : INetworkValidator
    {
        public bool IsValid(string ipNetwork)
        {
            try
            {
                var range = IPAddressRange.Parse(ipNetwork);
                return (range != null);
            }
            catch
            {
                return false;
            }
        }
    }
}