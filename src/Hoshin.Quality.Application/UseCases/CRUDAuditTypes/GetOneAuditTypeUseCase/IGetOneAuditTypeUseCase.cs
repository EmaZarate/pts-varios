using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetOneAuditTypeUseCase
{
    public interface IGetOneAuditTypeUseCase
    {
        AuditTypeOutput Execute(int id);
    }
}
