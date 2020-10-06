using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditStandardAspect.SetWithoutFindings
{
    public interface ISetWithoutFindingsAuditStandardAspectUseCase
    {
        bool Execute(Domain.AuditStandardAspect auditStandardAspect);
    }
}
