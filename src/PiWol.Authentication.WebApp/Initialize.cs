using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PiWol.Authentication.Data.Extensions;
using PiWol.Authentication.Data.Setup.Extensions;
using PiWol.Authentication.Extensions;
using PiWol.Core;

namespace PiWol.Authentication.WebApp
{
    public class Initialize : InitializeBase, IInitialize
    {
        public override int InitializeSequence { get; protected set; } = 100;

        public override IApplicationBuilder ConfigureApplication(IApplicationBuilder app)
        {
            app.UseDataSetup();
            app.UseAuthenticationServices();
            return base.ConfigureApplication(app);
        }

        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddDataSetup();
            services.AddData();
            services.AddAuthenticationServices();
            return base.ConfigureServices(services);
        }
    }
}