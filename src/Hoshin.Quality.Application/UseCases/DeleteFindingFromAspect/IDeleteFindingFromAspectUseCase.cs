using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.DeleteFindingFromAspect
{
    public interface IDeleteFindingFromAspectUseCase
    {
        bool Execute(int id);
    }
}
