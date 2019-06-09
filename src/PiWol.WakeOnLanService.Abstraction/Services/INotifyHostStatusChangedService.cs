using System.Threading.Tasks;
using PiWol.WakeOnLanService.Abstraction.Models;

namespace PiWol.WakeOnLanService.Abstraction.Services
{
    public interface INotifyHostStatusChangedService {
        Task Notify(HostModel model);

        bool ClientsConnected();
    }
}