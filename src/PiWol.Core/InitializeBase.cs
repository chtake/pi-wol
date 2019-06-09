using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PiWol.Core
{
    public abstract class InitializeBase : IInitialize
    {
        public virtual int InitializeSequence { get; protected set; } = 100000;

        public virtual IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services;
        }

        public virtual IApplicationBuilder ConfigureApplication(IApplicationBuilder app)
        {
            return app;
        }
    }
}