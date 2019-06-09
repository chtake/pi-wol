using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PiWol.Core
{
    public interface IInitialize
    {
        int InitializeSequence { get; }

        IServiceCollection ConfigureServices(IServiceCollection services);

        IApplicationBuilder ConfigureApplication(IApplicationBuilder app);
    }
}