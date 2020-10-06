using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllActivesAuditTypesUseCase
{
    public interface IGetAllActivesAuditTypesUseCase
    {
        Task<List<AuditTypeOutput>> Execute();
    }
}
