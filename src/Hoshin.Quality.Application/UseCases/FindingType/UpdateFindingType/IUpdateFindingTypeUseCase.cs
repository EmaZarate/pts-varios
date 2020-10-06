using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.UpdateFindingType
{
    public interface IUpdateFindingTypeUseCase
    {
        FindingTypeOutput Execute(Domain.FindingType.FindingType updateFindingType);
    }
}
