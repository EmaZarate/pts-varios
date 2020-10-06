using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Finding.GetAllFinding
{
    public interface IGetAllFindingUseCase
    {
        Task<List<FindingOutput>> Execute();
    }
}
