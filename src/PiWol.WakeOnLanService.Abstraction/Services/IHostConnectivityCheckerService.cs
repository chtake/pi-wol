using System.Collections.Generic;
using System.Threading.Tasks;
using PiWol.WakeOnLanService.Abstraction.Models;

namespace PiWol.WakeOnLanService.Abstraction.Services
{
    public interface IHostConnectivityCheckerService
    {
        Task<HostStatus> CheckHost(string ipAddr);

        Task<HostModel> CheckHost(HostModel model);

        Task<IEnumerable<HostModel>> CheckHosts(IEnumerable<HostModel> models);
    }
}