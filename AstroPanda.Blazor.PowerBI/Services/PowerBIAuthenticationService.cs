using AstroPanda.Blazor.PowerBI.Options;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Rest;

namespace AstroPanda.Blazor.PowerBI.Services
{
    public class PowerBIAuthenticationService : IPowerBIAuthenticationService
    {
        private PowerBIOptions _options;

        public PowerBIAuthenticationService(IOptionsMonitor<PowerBIOptions> optionsMonitor)
        {
            _options = optionsMonitor.CurrentValue;
        }

        public async Task<TokenCredentials> GetAuthentication()
        {
            IConfidentialClientApplication daemonClient = ConfidentialClientApplicationBuilder.Create(_options.ClientId)
                                                                                              .WithAuthority(_options.Authority)
                                                                                              .WithClientSecret(_options.ClientSecret)
                                                                                              .Build();

            AuthenticationResult authResult = await daemonClient.AcquireTokenForClient(new[] { PowerBIOptions.MSGraphScope }).ExecuteAsync();

            return new TokenCredentials(authResult.AccessToken, authResult.TokenType);
        }
    }
}
