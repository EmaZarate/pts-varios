using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ApproveRejectAudit
{
    public class ApproveRejectAuditUseCase : IApproveRejectAuditUseCase
    {
        private readonly IWorkflowCore _workflowCore;

        public ApproveRejectAuditUseCase(IWorkflowCore workflowCore)
        {
            _workflowCore = workflowCore;
        }

        public Task<bool> Execute(object auditWorkflowData)
        {
            try
            {

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Execute(AuditWorkflowData audit)
        {
            await _workflowCore.ExecuteEvent("Approve", audit.WorkflowId, audit);
            return true;
        }
    }
}
