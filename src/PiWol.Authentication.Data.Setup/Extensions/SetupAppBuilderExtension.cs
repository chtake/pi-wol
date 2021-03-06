﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PiWol.Authentication.Data.Setup.Services;

namespace PiWol.Authentication.Data.Setup.Extensions
{
    public static class SetupAppBuilderExtension
    {
        public static IApplicationBuilder UseDataSetup(this IApplicationBuilder app)
        {
            var setupService = app.ApplicationServices.GetRequiredService<DataStorageSetupService>();
            setupService.CreateOrMigrateDataStorage();

            return app;
        }
    }
}