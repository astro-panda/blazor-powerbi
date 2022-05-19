using AstroPanda.Blazor.PowerBI.Options;
using AstroPanda.Blazor.PowerBI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
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
            services.Configure<PowerBIOptions>(x => configuration.Bind(x));
            services.AddTransient<PowerBIInterop>();
            services.AddTransient<IPowerBIAuthenticationService, PowerBIAuthenticationService>();
            services.AddTransient<PowerBIClient>(sp =>
            {
                var auth = sp.GetRequiredService<IPowerBIAuthenticationService>();
                var authResult = auth.GetAuthentication().GetAwaiter().GetResult();
                var creds = new TokenCredentials(authResult.AccessToken, "Bearer");
                return new PowerBIClient(creds);
            });
            return services;
        }
    }
}
