using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditState.GetOneAuditState
{
    public interface IGetOneAuditStateUseCase
    {
        AuditStateOutput Execute(int id);
    }
}
