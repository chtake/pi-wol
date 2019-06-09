using System.Collections.Generic;
using PiWol.WakeOnLanService.Abstraction.Models;

namespace PiWol.WakeOnLanService.Abstraction.Services
{
    public interface IHostStatusCacheService
    {
        bool Delete(string ip);

        void AddOrUpdate(string ip, HostStatus status);

        HostStatus Get(string ip);

        IEnumerable<string> GetKeys();
    }
}