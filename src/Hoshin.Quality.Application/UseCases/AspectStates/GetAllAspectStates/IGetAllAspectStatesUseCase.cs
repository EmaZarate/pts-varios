using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AspectStates.GetAllAspectStates
{
    public interface IGetAllAspectStatesUseCase
    {
       List<AspectStatesOutput> Execute();
    }
}
