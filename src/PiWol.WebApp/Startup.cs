using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using PiWol.Core;

namespace PiWol.WebApp
{
    public class Startup
    {
        private List<IInitialize> _startupServices = new List<IInitialize>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var physicalProvider =
                new PhysicalFileProvider(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            services.AddSingleton<IFileProvider>(physicalProvider);

            var serviceAssemblies = LoadWebAppAssemblies().ToList();
            var mvcBuilder = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            foreach (var assembly in serviceAssemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
                var t = assembly.DefinedTypes.FirstOrDefault(x =>
                        x.ImplementedInterfaces.Any(i => i == typeof(IInitialize)) && !x.IsAbstract)
                    ?.AsType();
                if (t != null)
                {
                    services.AddSingleton(typeof(IInitialize), t);
                }
            }

            var sp = services.BuildServiceProvider();
            _startupServices = sp.GetServices<IInitialize>().OrderBy(x => x.InitializeSequence).ToList();

            foreach (var startup in _startupServices)
            {
                startup.ConfigureServices(services);
            }

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(options =>
                {
                    // options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                    options.AllowCredentials();
                    options.WithOrigins("http://localhost:4200");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            foreach (var startupService in _startupServices)
            {
                startupService.ConfigureApplication(app);
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}"); });

            if (env.IsDevelopment() == false)
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        // spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            }
        }

        private static IEnumerable<Assembly> LoadWebAppAssemblies()
        {
            var entryLocation = Assembly.GetEntryAssembly().Location;
            var path = Path.GetDirectoryName(entryLocation);

            if (path == null)
            {
                return Enumerable.Empty<Assembly>();
            }

            return Directory.GetFiles(path, "*.WebApp.dll", SearchOption.TopDirectoryOnly)
                .Where(x => x != entryLocation).Select(LoadAssembly)
                .ToList();
        }

        private static Assembly LoadAssembly(string path)
        {
            return Assembly.LoadFrom(path);
        }
    }
}