using Microsoft.Extensions.DependencyInjection;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Infrastructure.SignalR.Services;

namespace PiWol.WakeOnLanService.Infrastructure.SignalR.Extensions
{
    public static class SignalRServiceCollectionExtension
    {
        public static IServiceCollection AddEventHub(this IServiceCollection service)
        {
            service.AddSignalR();

            service.AddSingleton<HubUserHandler>();
            service.AddSingleton<INotifyHostStatusChangedService, NotifyHostStatusChangedService>();

            return service;
        }
    }
}