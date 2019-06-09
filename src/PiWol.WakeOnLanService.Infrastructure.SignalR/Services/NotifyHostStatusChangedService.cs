using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PiWol.WakeOnLanService.Abstraction.Models;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Infrastructure.SignalR.Hubs;

namespace PiWol.WakeOnLanService.Infrastructure.SignalR.Services
{
    public class NotifyHostStatusChangedService : INotifyHostStatusChangedService
    {
        private const string Event = "HostStatusChanged";

        private readonly IHubContext<HostStatusHub> _hub;

        private readonly HubUserHandler _userHandler;

        public NotifyHostStatusChangedService(IHubContext<HostStatusHub> hub, HubUserHandler userHandler)
        {
            _hub = hub;
            _userHandler = userHandler;
        }

        public async Task Notify(HostModel model)
        {
            await _hub.Clients.All.SendAsync(Event, model).ConfigureAwait(false);
        }

        public bool ClientsConnected()
        {
            return _userHandler.ConnectedClients() > 0;
        }
    }
}