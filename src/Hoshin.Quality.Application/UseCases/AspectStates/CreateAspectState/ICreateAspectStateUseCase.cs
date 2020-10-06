using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AspectStates.CreateAspectState
{
    public interface ICreateAspectStateUseCase
    {
        AspectStatesOutput Execute(string name, string colour, bool active);
    }
}
