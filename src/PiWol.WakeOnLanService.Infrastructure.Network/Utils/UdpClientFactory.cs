using System.Net.Sockets;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Utils
{
    internal class UdpClientFactory : IUdpClientFactory
    {
        public UdpClient CreateNewInstance()
        {
            return new UdpClient();
        }
    }
}