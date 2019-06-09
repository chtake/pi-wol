using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PiWol.Core;
using PiWol.WakeOnLanService.Data.Extensions;
using PiWol.WakeOnLanService.Data.Setup.Extensions;
using PiWol.WakeOnLanService.Extensions;
using PiWol.WakeOnLanService.Infrastructure.Network.Extensions;
using PiWol.WakeOnLanService.Infrastructure.SignalR.Extensions;

namespace PiWol.WakeOnLanService.WebApp
{
    public class Initialize : InitializeBase, IInitialize
    {
        public override IApplicationBuilder ConfigureApplication(IApplicationBuilder app)
        {
            app.UseDataSetup();
            app.UseEventHub();
            app.UseNetworkServices();

            return base.ConfigureApplication(app);
        }

        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddDataSetup();
            services.AddData();
            services.AddWolServices();
            services.AddNetworkServices();
            services.AddEventHub();

            return base.ConfigureServices(services);
        }
    }
}