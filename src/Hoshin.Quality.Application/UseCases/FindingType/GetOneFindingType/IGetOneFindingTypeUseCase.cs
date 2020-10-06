using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.GetOneFindingType
{
    public interface IGetOneFindingTypeUseCase
    {
        FindingTypeOutput Execute(int id);
    }
}
