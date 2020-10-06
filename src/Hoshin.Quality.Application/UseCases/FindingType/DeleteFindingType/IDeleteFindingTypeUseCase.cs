using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.DeleteFindingType
{
    public interface IDeleteFindingTypeUseCase
    {
        bool Execute(int id);
    }
}
