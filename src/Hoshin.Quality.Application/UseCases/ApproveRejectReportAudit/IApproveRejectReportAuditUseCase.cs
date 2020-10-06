using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ApproveRejectReportAudit
{
    public interface IApproveRejectReportAuditUseCase
    {
        Task<bool> Execute(AuditWorkflowData audit);
    }
}
