using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditState.CreateAuditState
{
    public interface ICreateAuditStateUseCase
    {
        AuditStateOutput Execute(Domain.AuditState.AuditState auditState);
    }
}
