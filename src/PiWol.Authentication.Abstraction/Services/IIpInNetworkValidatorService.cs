using System.Net;

namespace PiWol.Authentication.Abstraction.Services
{
    public interface IIpInNetworkValidatorService
    {
        bool IsInRange(string ipAddress, string cidrNetwork);
        bool IsInRange(IPAddress ipAddress, string cidrNetwork);
        string ParseRange(string cidrNetwork);
    }
}