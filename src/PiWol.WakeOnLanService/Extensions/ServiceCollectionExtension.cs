using Microsoft.Extensions.DependencyInjection;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Services;

namespace PiWol.WakeOnLanService.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWolServices(this IServiceCollection services)
        {
            services.AddScoped<IHostService, HostService>();
            return services;
        }
    }
}