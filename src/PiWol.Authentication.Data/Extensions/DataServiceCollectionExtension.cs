using Microsoft.Extensions.DependencyInjection;
using PiWol.Authentication.Data.Abstraction.Repositories;
using PiWol.Authentication.Data.Repositories;

namespace PiWol.Authentication.Data.Extensions
{
    public static class DataServiceCollectionExtension
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddSingleton<IIpNetworkRepository, IpNetworkJsonRepository>();
            return services;
        }
    }
}