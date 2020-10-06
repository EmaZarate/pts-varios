using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates.UpdateFindingsStates
{
    public interface IUpdateFindingsStatesUseCase
    {
        FindingsStatesOutput Execute(int id, string code, string name, string colour, bool active);
    }
}
