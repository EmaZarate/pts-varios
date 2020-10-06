using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDSector.GetAllSectorsUseCase
{
    public interface IGetAllSectorsUseCase
    {
        Task<List<SectorOutput>> Execute();
    }
}
