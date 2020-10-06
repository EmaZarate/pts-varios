using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates.GetOneFindingsStates
{
    public interface IGetOneFindingsStatesUseCase
    {
        FindingsStatesOutput Execute(int id);
    }
}
