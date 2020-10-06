using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllAuditTypesUseCase
{
    public interface IGetAllAuditTypesUseCase
    {
        Task<List<AuditTypeOutput>> Execute();
    }
}
