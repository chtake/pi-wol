using FluentScheduler;
using Microsoft.AspNetCore.Builder;
using PiWol.WakeOnLanService.Infrastructure.Network.Scheduler;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Extensions
{
    public static class NetworkApplicationBuilder
    {
        public static IApplicationBuilder UseNetworkServices(this IApplicationBuilder app)
        {
            JobManager.Initialize(new WolRegistry(app.ApplicationServices));

            return app;
        }
    }
}