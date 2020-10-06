using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditState.UpdateAuditState
{
    public interface IUpdateAuditStateUseCase
    {
        AuditStateOutput Execute(Domain.AuditState.AuditState auditState);
    }
}
