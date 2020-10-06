using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AspectStates.GetOneAspectState
{
    public interface IGetOneAspectStateUseCase
    {
        AspectStatesOutput Execute(int id);
    }
}
