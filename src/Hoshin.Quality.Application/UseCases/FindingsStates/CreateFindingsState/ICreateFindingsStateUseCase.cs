using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates.CreateFindingsStateUseCase
{
    public interface ICreateFindingsStateUseCase
    {
        FindingsStatesOutput Execute(string code, string name, string colour, bool active);
    }
}
