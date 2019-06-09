using System.Net.Sockets;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Utils
{
    public interface IUdpClientFactory
    {
        UdpClient CreateNewInstance();
    }
}