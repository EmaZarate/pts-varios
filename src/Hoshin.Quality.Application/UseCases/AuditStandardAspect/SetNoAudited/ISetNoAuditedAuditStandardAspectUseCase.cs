using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditStandardAspect.SetNoAudited
{
    public interface ISetNoAuditedAuditStandardAspectUseCase
    {
        bool Execute(Domain.AuditStandardAspect auditStandardAspect);
    }
}
