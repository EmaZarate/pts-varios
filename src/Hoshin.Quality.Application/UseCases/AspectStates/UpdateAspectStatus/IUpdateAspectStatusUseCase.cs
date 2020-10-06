using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Application.UseCases.AspectStates;
namespace Hoshin.Quality.Application.UseCases.AspectStates.UpdateAspectStatus
{
    public interface IUpdateAspectStatusUseCase
    {
        AspectStatesOutput Execute(int id, string name, string colour, bool active );
    }
}
