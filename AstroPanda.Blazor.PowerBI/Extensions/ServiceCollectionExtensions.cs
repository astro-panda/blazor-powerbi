using AstroPanda.Blazor.PowerBI.Options;
using AstroPanda.Blazor.PowerBI.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorPowerBI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PowerBIOptions>(x => configuration.Bind(x));
            services.AddTransient<PowerBIInterop>();
            services.AddTransient<IPowerBIAuthenticationService, PowerBIAuthenticationService>();
            return services;
        }
    }
}
