using Microsoft.Extensions.DependencyInjection;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Abstraction.Validators;
using PiWol.WakeOnLanService.Infrastructure.Network.Services;
using PiWol.WakeOnLanService.Infrastructure.Network.Utils;
using PiWol.WakeOnLanService.Infrastructure.Network.Validators;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Extensions
{
    public static class NetworkServiceCollectionExtension
    {
        public static IServiceCollection AddNetworkServices(this IServiceCollection services)
        {
            services.AddSingleton<IHostConnectivityCheckerService, HostConnectivityCheckerService>();
            services.AddSingleton<IHostStatusCacheService, HostStatusCacheService>();
            services.AddSingleton<IMacAddressResolverService, MacAddressResolverService>();
            services.AddSingleton<ISubnetMaskValidator, SubnetMaskValidator>();
            services.AddSingleton<IIpNetworkCalculationsService, IpNetworkCalculationsService>();
            services.AddSingleton<IUdpClientFactory, UdpClientFactory>();
            services.AddSingleton<IWakeUpService, WakeUpService>();

            return services;
        }
    }
}