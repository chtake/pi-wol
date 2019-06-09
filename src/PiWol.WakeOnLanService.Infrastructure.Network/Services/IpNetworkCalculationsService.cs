using System;
using System.Collections;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Logging;
using PiWol.WakeOnLanService.Abstraction.Services;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Services
{
    public class IpNetworkCalculationsService : IIpNetworkCalculationsService
    {
        private readonly ILogger _logger;

        public IpNetworkCalculationsService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public string GetBroadcastAddress(string ipAddr, string netmask)
        {
            if (TryIpToBitOctets(netmask, out var subnetBits) == false)
            {
                throw new Exception("Netmask is not valid.");
            }

            if (IsSubnetMaskValid(subnetBits) == false)
            {
                throw new Exception("Netmask is not valid.");
            }

            if (TryIpToBitOctets(ipAddr, out var ipBits) == false)
            {
                throw new Exception("Ip is not valid.");
            }

            var subnetBitsInverted = subnetBits.Not();
            ipBits = ipBits.Or(subnetBitsInverted);

            var ip = new int[4];
            var idx = -1;
            for (var i = 0; i < ipBits.Count; i++)
            {
                if (i % 8 == 0)
                {
                    idx++;
                }

                ip[idx] += (ipBits[i]) ? (int)Math.Pow(2, (7 - (i % 8))) : 0;
            }

            return string.Join('.', ip);
        }

        public bool IsSubnetMaskValid(string netmask)
        {
            _logger.LogDebug($"Validating {netmask}");

            var bitsArray = IpToBitOctets(netmask);
            return IsSubnetMaskValid(bitsArray);
        }

        public bool IsSubnetMaskValid(BitArray bits)
        {
            if (bits == null)
            {
                return false;
            }

            int i;
            for (i = 0; i < 32 && bits[i]; i++) { }

            if (i < 8)
            {
                return false;
            }

            for (; i < 32; i++)
            {
                if (bits[i])
                {
                    return false;
                }
            }

            return true;
        }

        public BitArray IpToBitOctets(string ip)
        {
            if (!TryIpToBitOctets(ip, out var bits))
            {
                return null;
            }

            return bits;
        }

        public bool TryIpToBitOctets(string ip, out BitArray bits)
        {
            if (IPAddress.TryParse(ip, out var addr) == false)
            {
                bits = new BitArray(0);
                return false;
            }

            var octets = addr.GetAddressBytes().Select(o => Convert.ToString(o, 2).PadLeft(8, '0').Select(b => b == '1').ToArray()).ToArray();

            bits = new BitArray(octets.SelectMany(x => x.Select(y => y).ToArray()).ToArray());
            return true;
        }
    }
}