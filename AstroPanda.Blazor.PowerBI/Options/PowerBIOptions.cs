using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroPanda.Blazor.PowerBI.Options
{
    public class PowerBIOptions
    {
        public const string MSGraphScope = "https://analysis.windows.net/powerbi/api/.default";


        public string TenantId { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;

        public string Authority => $"https://login.microsoftonline.com/{TenantId}/v2.0";

        public Dictionary<string, ReportReference> Reports { get; set; } = new();
    }
}
