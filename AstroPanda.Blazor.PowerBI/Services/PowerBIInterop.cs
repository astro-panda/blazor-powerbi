using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroPanda.Blazor.PowerBI.Services
{
    public class PowerBIInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;        
        private readonly Lazy<Task<IJSObjectReference>> embedModuleTask;        
        private readonly PowerBIClient _powerBi;

        public PowerBIInterop(IJSRuntime jsRuntime, PowerBIClient powerBI)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/powerbi-client/dist/powerbi.js").AsTask());
            embedModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/blazor-power-bi.js").AsTask());
            _powerBi = powerBI;
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }

            if (embedModuleTask.IsValueCreated)
            {
                var module = await embedModuleTask.Value;
                await module.DisposeAsync();
            }
        }
        public async Task GenerateReport(ElementReference reportContainer, Guid workspaceId, Guid reportId)
        {
            Report report = await _powerBi.Reports.GetReportInGroupAsync(workspaceId, reportId);

            var module = await embedModuleTask.Value;
            await module.InvokeAsync<object>("embedReport");
        }
    }
}
