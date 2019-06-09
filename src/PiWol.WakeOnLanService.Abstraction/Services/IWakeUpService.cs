using System.Threading.Tasks;
using PiWol.WakeOnLanService.Abstraction.Models;

namespace PiWol.WakeOnLanService.Abstraction.Services
{
    public interface IWakeUpService
    {
        Task SendMagicPacket(HostModel model);
    }
}