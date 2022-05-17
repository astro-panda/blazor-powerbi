using AstroPanda.Blazor.PowerBI.Options;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace AstroPanda.Blazor.PowerBI.Services
{
    public class PowerBIAuthenticationService : IPowerBIAuthenticationService
    {
        private PowerBIOptions _options;

        public PowerBIAuthenticationService(IOptionsMonitor<PowerBIOptions> optionsMonitor)
        {
            _options = optionsMonitor.CurrentValue;
        }

        public async Task<AuthenticationResult> GetAuthentication()
        {
            IConfidentialClientApplication daemonClient = ConfidentialClientApplicationBuilder.Create(_options.ClientId)
                                                                                              .WithAuthority(_options.Authority)
                                                                                              .WithClientSecret(_options.ClientSecret)
                                                                                              .Build();

            return await daemonClient.AcquireTokenForClient(new[] { PowerBIOptions.MSGraphScope }).ExecuteAsync();
        }
    }
}
