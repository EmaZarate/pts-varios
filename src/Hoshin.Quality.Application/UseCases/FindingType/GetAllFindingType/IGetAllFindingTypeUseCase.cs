using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.GetAllFindingType
{
    public interface IGetAllFindingTypeUseCase
    {
        List<FindingTypeOutput> Execute();
    }
}
