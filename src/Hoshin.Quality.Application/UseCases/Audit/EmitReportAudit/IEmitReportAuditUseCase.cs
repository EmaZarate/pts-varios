using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Audit.EmitReportAudit
{
    public interface IEmitReportAuditUseCase
    {
        Task<bool> Execute(AuditWorkflowData audit);
    }
}
