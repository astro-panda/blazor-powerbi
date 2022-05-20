using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroPanda.Blazor.PowerBI.Options
{
    public class ReportReference
    {
        public Guid WorkspaceId { get; set; }

        public Guid ReportId { get; set; }
    }
}
