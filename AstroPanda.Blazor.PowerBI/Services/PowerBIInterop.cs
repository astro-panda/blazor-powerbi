using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;

namespace AstroPanda.Blazor.PowerBI.Services
{
    public class PowerBIInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _embedModuleTask;
        private readonly IPowerBIAuthenticationService _authService;

        public PowerBIInterop(IJSRuntime jsRuntime, IPowerBIAuthenticationService authService)
        {
            _embedModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/AstroPanda.Blazor.PowerBI/blazor-power-bi.js").AsTask());
            _authService = authService;
        }

        public async ValueTask DisposeAsync()
        {
            if (_embedModuleTask.IsValueCreated)
            {
                var module = await _embedModuleTask.Value;
                await module.DisposeAsync();
            }
        }

        public async Task GenerateReport(ElementReference reportContainer, Guid workspaceId, Guid reportId)
        {
            var tokenCreds = await _authService.GetAuthentication();

            using PowerBIClient _powerBI = new PowerBIClient(new Uri("https://api.powerbi.com"), tokenCreds);
            Report report = await _powerBI.Reports.GetReportInGroupAsync(workspaceId, reportId);

            var tokenRequest = new GenerateTokenRequest(TokenAccessLevel.View, report.DatasetId);            
            var embedToken = await _powerBI.Reports.GenerateTokenInGroupAsync(workspaceId, reportId, tokenRequest);

            var module = await _embedModuleTask.Value;
            await module.InvokeAsync<object>("embedReport", reportContainer, reportId, report.EmbedUrl, embedToken.Token);
        }
    }
}
