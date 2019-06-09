using Microsoft.Extensions.DependencyInjection;
using PiWol.Authentication.Abstraction.Services;
using PiWol.Authentication.Abstraction.Validators;
using PiWol.Authentication.Infrastructure.IpUtils.Services;
using PiWol.Authentication.Infrastructure.IpUtils.Validators;

namespace PiWol.Authentication.Infrastructure.IpUtils.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIpUtilsServices(this IServiceCollection services)
        {
            services.AddSingleton<IIpInNetworkValidatorService, IpInNetworkValidatorService>();
            services.AddSingleton<INetworkValidator, NetworkValidator>();
            return services;
        }
    }
}