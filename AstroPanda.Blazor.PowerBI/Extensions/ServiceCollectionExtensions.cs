using AstroPanda.Blazor.PowerBI.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddBlazorPowerBI(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddBlazorPowerBI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PowerBIOptions>(x => configuration.GetSection("PowerBI"));
            return services;
        }
    }
}
