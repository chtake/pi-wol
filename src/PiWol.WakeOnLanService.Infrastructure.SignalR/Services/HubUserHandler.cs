using System.Collections.Generic;

namespace PiWol.WakeOnLanService.Infrastructure.SignalR.Services
{
    public class HubUserHandler
    {
        public static readonly HashSet<string> ConnectedIds = new HashSet<string>();

        public void AddClient(string connectionId)
        {
            ConnectedIds.Add(connectionId);
        }

        public void DeleteClient(string connectionId)
        {
            ConnectedIds.Remove(connectionId);
        }

        public int ConnectedClients()
        {
            return ConnectedIds.Count;
        }
    }
}