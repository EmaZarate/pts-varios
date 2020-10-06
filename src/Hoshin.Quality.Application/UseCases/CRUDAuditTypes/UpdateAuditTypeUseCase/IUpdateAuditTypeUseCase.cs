using Hoshin.Quality.Domain.AuditType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.UpdateAuditTypeUseCase
{
    public interface IUpdateAuditTypeUseCase
    {
        AuditTypeOutput Execute(AuditType auditType);
    }
}
