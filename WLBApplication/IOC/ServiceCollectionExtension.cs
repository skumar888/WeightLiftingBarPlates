using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WLBApplication.Application;
using WLBApplication.Cache;

namespace WLBApplication.IOC
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJsonParser, JsonParser>();
            services.AddScoped<IInputValidatorAndParser, InputValidatorAndParser>();
            services.AddScoped<IGetMinimumPlates, GetMinimumPlates>();
            services.AddSingleton<IWLBMinWeightCache, WLBMinWeightCache>();
        }

    }
}
