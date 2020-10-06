using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Audit.DeleteAudit
{
    public interface IDeleteAuditUseCase
    {
        Task<bool> Execute(int auditId, string workflowId);
    }
}
