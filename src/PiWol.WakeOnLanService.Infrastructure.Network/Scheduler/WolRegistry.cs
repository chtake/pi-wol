using System;
using FluentScheduler;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Scheduler
{
    public class WolRegistry : Registry
    {
        public WolRegistry(IServiceProvider provider)
        {
            Schedule(() => new PingHostsJob(provider)).NonReentrant().ToRunNow().AndEvery(5).Seconds();
        }
    }
}