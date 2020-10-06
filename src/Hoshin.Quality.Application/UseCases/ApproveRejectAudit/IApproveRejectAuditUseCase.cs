using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ApproveRejectAudit
{
    public interface IApproveRejectAuditUseCase
    {
        Task<bool> Execute(AuditWorkflowData audit);
        //Task<bool> Execute(object approveAudit);
    }
}
