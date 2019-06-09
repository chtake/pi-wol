using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PiWol.WakeOnLanService.Infrastructure.SignalR.Services;

namespace PiWol.WakeOnLanService.Infrastructure.SignalR.Hubs
{
    public class HostStatusHub : Hub
    {
        private readonly HubUserHandler _userHandler;

        public HostStatusHub(HubUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        public override Task OnConnectedAsync()
        {
            _userHandler.AddClient(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _userHandler.DeleteClient(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}