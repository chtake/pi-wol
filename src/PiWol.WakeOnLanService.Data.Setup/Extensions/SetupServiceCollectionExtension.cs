using Microsoft.Extensions.DependencyInjection;
using PiWol.WakeOnLanService.Data.Setup.Services;

namespace PiWol.WakeOnLanService.Data.Setup.Extensions
{
    public static class SetupServiceCollectionExtension
    {
        public static IServiceCollection AddDataSetup(this IServiceCollection services)
        {
            services.AddSingleton<DataStorageSetupService>();
            return services;
        }
    }
}