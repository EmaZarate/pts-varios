using Hoshin.Quality.Domain.AuditType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.AddAuditTypeUseCase
{
    public interface IAddAuditTypeUseCase
    {
        AuditTypeOutput Execute(AuditType auditType);
    }
}
