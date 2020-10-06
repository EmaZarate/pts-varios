using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditState.GetAllAuditState
{
    public interface IGetAllAuditStateUseCase
    {
        List<AuditStateOutput> Execute();
    }
}
