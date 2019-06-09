using System;
using System.Threading.Tasks;
using PiWol.WakeOnLanService.Abstraction.Models;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Infrastructure.Network.Utils;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Services
{
    internal class WakeUpService : IWakeUpService
    {
        private readonly IIpNetworkCalculationsService _networkService;

        private readonly IUdpClientFactory _udpClientFactory;

        public WakeUpService(IIpNetworkCalculationsService networkService, IUdpClientFactory udpClientFactory)
        {
            _networkService = networkService;
            _udpClientFactory = udpClientFactory;
        }

        public async Task SendMagicPacket(HostModel model)
        {
            var broadcastAddress = _networkService.GetBroadcastAddress(model.IpAddress, model.Netmask);
            var mac = MacAddressToBytes(model.MacAddress);
            var macLength = mac.Length;

            var packetBytes = new byte[6 + 16 * macLength];

            for (var i = 0; i < 6; i++)
            {
                packetBytes[i] = 0xff;
            }

            for (var i = 6; i < packetBytes.Length; i += macLength)
            {
                Array.Copy(mac, 0, packetBytes, i, macLength);
            }

            using (var client = _udpClientFactory.CreateNewInstance())
            {
                client.EnableBroadcast = true;
                client.Connect(broadcastAddress, 7);
                await client.SendAsync(packetBytes, packetBytes.Length).ConfigureAwait(false);
            }
        }

        private byte[] MacAddressToBytes(string macAddress)
        {
            var bytes = new byte[6];

            var macSplitted = macAddress.Replace('-', ':').Split(':');
            if (macSplitted.Length != bytes.Length)
            {
                throw new Exception("Invalid mac address");
            }

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)Convert.ToUInt32(macSplitted[i], 16);
            }

            return bytes;
        }
    }
}