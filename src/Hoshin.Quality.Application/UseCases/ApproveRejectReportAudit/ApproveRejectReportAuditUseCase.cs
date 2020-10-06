using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ApproveRejectReportAudit
{
    public class ApproveRejectReportAuditUseCase : IApproveRejectReportAuditUseCase
    {
        private readonly IWorkflowCore _workflowCore;

        public ApproveRejectReportAuditUseCase(IWorkflowCore workflowCore)
        {
            _workflowCore = workflowCore;
        }


        public async Task<bool> Execute(AuditWorkflowData audit)
        {

            await _workflowCore.ExecuteEvent("ApproveReport", audit.WorkflowId, audit);
            return true;
        }
    }
}
