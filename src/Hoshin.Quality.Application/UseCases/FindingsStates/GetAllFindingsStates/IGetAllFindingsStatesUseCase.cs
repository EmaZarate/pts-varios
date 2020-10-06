using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates.GetAllFindingsStates
{
    public interface IGetAllFindingsStatesUseCase
    {
        List<FindingsStatesOutput> Execute();
    }
}
