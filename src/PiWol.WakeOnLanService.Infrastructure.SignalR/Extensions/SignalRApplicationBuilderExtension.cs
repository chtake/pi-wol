using Microsoft.AspNetCore.Builder;
using PiWol.WakeOnLanService.Infrastructure.SignalR.Hubs;

namespace PiWol.WakeOnLanService.Infrastructure.SignalR.Extensions
{
    public static class SignalRApplicationBuilderExtension
    {
        public static IApplicationBuilder UseEventHub(this IApplicationBuilder app)
        {
            app.UseSignalR(routes => { routes.MapHub<HostStatusHub>("/api/wol/hub/host-status"); });

            return app;
        }
    }
}