using System;
using System.Linq;
using System.Threading.Tasks;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PiWol.WakeOnLanService.Abstraction.Models;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Data.Abstraction.Repositories;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Scheduler
{
    public class PingHostsJob : IJob
    {
        private readonly IHostStatusCacheService _cache;

        private readonly IHostService _hostService;

        private readonly ILogger _logger;

        private readonly INotifyHostStatusChangedService _notify;

        private readonly IHostConnectivityCheckerService _ping;

        private readonly IHostRepository _repository;

        public PingHostsJob(IServiceProvider provider)
        {
            _repository = provider.GetService<IHostRepository>();
            _hostService = provider.GetService<IHostService>();
            _cache = provider.GetService<IHostStatusCacheService>();
            _ping = provider.GetService<IHostConnectivityCheckerService>();
            _logger = provider.GetService<ILoggerFactory>().CreateLogger(GetType());
            _notify = provider.GetService<INotifyHostStatusChangedService>();
        }

        public void Execute()
        {
            ExecuteAsync().Wait();
        }

        public async Task ExecuteAsync()
        {
            if (_notify.ClientsConnected() == false)
            {
                _logger.LogDebug("No Clients connected, abort job.");
                return;
            }

            var hosts = (await _hostService.ReadAll().ConfigureAwait(false)).ToList(); //_repository.ReadAll().ToList();
            var cachedKeys = _cache.GetKeys().ToList();

            var removeKeys = cachedKeys.Except(hosts.Select(x => x.IpAddress));
            foreach (var key in removeKeys)
            {
                _cache.Delete(key);
                _logger.LogDebug($"Removed {key} from cache.");
            }

            foreach (var host in hosts)
            {
                var res = await _ping.CheckHost(host.IpAddress).ConfigureAwait(false);

                if (_cache.Get(host.IpAddress) != res)
                {
                    _cache.AddOrUpdate(host.IpAddress, res);
                    host.Status = res;
                    NotifyChanged(host);
                }
            }
        }

        private void NotifyChanged(HostModel host)
        {
            _notify.Notify(host);
            _logger.LogDebug("Status Changed: " + host.IpAddress + " => " + host.Status);
        }
    }
}