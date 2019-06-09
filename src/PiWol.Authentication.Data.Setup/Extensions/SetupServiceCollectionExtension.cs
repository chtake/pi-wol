using Microsoft.Extensions.DependencyInjection;
using PiWol.Authentication.Data.Setup.Services;

namespace PiWol.Authentication.Data.Setup.Extensions
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