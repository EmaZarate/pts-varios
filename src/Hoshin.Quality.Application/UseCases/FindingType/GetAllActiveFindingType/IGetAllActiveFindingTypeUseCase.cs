using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.GetAllActiveFindingType
{
    public interface IGetAllActiveFindingTypeUseCase
    {
        List<FindingTypeOutput> Execute();
    }
}
