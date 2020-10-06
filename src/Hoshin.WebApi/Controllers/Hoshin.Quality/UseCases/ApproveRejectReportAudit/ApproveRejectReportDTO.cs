using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveRejectReportAudit
{
    public class ApproveRejectReportDTO
    {
            public string WorkFlowId { get; set; }
            public string ApproveReportComments { get; set; }
            public int AuditID { get; set; }
            public string EventData { get; set; }
    }
}
