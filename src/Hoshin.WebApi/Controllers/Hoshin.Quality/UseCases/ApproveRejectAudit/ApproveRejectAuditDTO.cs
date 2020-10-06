using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveRejectAudit
{
    public class ApproveRejectAuditDTO
    {
        public string WorkFlowId { get; set; }
        public int AuditStateID { get; set; }
        public string ApprovePlanComments { get; set; }
        public int AuditID { get; set; }
        public string EventData { get; set; }
    }
}
