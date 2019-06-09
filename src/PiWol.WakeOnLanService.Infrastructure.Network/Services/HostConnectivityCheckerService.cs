using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using PiWol.WakeOnLanService.Abstraction.Models;
using PiWol.WakeOnLanService.Abstraction.Services;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Services
{
    internal class HostConnectivityCheckerService : IHostConnectivityCheckerService
    {
        public async Task<HostStatus> CheckHost(string ipAddr)
        {
            var pingSender = new Ping();

            var reply = await pingSender.SendPingAsync(ipAddr, 100).ConfigureAwait(false);

            return (reply.Status == IPStatus.Success) ? HostStatus.Online : HostStatus.Offline;
        }

        public async Task<HostModel> CheckHost(HostModel model)
        {
            model.Status = await CheckHost(model.IpAddress).ConfigureAwait(false);
            return model;
        }

        public async Task<IEnumerable<HostModel>> CheckHosts(IEnumerable<HostModel> models)
        {
            var tasks = models.ToList().Select(CheckHost);
            return await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}