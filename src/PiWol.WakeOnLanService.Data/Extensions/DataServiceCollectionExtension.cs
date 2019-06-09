using Microsoft.Extensions.DependencyInjection;
using PiWol.WakeOnLanService.Data.Abstraction.Repositories;
using PiWol.WakeOnLanService.Data.Repositories;

namespace PiWol.WakeOnLanService.Data.Extensions
{
    public static class DataServiceCollectionExtension
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddSingleton<IHostRepository, HostJsonRepository>();
            return services;
        }
    }
}