using System.Collections;

namespace PiWol.WakeOnLanService.Abstraction.Services
{
    public interface IIpNetworkCalculationsService
    {
        string GetBroadcastAddress(string ipAddr, string netmask);

        bool IsSubnetMaskValid(string netmask);

        bool IsSubnetMaskValid(BitArray bits);

        BitArray IpToBitOctets(string ip);

        bool TryIpToBitOctets(string ip, out BitArray bits);
    }
}