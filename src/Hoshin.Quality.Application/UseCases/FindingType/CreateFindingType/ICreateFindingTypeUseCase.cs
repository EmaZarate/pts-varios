using Hoshin.Quality.Domain.FindingType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.CreateFindingType
{
    public interface ICreateFindingTypeUseCase
    {
        FindingTypeOutput Execute(string name, string code, bool active, List<FindingTypeParametrization> parametrization);
    }
}
