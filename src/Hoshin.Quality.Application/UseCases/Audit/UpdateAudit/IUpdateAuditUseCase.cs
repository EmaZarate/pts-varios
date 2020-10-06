using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Audit.UpdateAudit
{
    public interface IUpdateAuditUseCase
    {
        bool Execute(Domain.Audit.Audit audit);
    }
}
