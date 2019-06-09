using System.Collections.Generic;
using System.Linq;
using PiWol.WakeOnLanService.Abstraction.Models;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Data.Abstraction.Repositories;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Services
{
    internal class HostStatusCacheService : IHostStatusCacheService
    {
        private readonly IDictionary<string, HostStatus> _cache = new Dictionary<string, HostStatus>();

        private readonly IHostRepository _repository;

        public HostStatusCacheService(IHostRepository repository)
        {
            _repository = repository;
        }

        public bool Delete(string ip)
        {
            return _cache.Remove(ip);
        }

        public void AddOrUpdate(string ip, HostStatus status)
        {
            _cache[ip] = status;
        }

        public HostStatus Get(string ip)
        {
            if (_cache.ContainsKey(ip) == false)
            {
                _cache[ip] = HostStatus.Unknown;
            }

            return _cache[ip];
        }

        public IEnumerable<string> GetKeys()
        {
            return _cache.Select(x => x.Key);
        }
    }
}