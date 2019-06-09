using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PiWol.Authentication.Abstraction.Services;
using PiWol.Authentication.Handler;
using PiWol.Authentication.Infrastructure.IpUtils.Extensions;
using PiWol.Authentication.Services;
using AuthenticationService = PiWol.Authentication.Services.AuthenticationService;
using IAuthenticationService = PiWol.Authentication.Abstraction.Services.IAuthenticationService;

namespace PiWol.Authentication.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddAuthentication(AuthenticationDefaults.AuthenticationScheme)
                .AddScheme<PiWolAuthenticationOptions, PiWolAuthenticationHandler>(
                    AuthenticationDefaults.AuthenticationScheme, null);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationMethodService, IpRestrictedAuthenticationMethodService>();
            services.AddScoped<IIpNetworkService, IpNetworkService>();

            services.AddIpUtilsServices();

            return services;
        }

        public static IApplicationBuilder UseAuthenticationServices(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.Use(async (context, next) =>
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    await context.ChallengeAsync().ConfigureAwait(true);
                }
                else
                {
                    await next().ConfigureAwait(true);
                }
            });
            return app;
        }
    }
}