using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetAllCorrectiveActionStates
{
    public interface IGetAllCorrectiveActionStatesUseCase
    {
        List<CorrectiveActionStateOutput> Execute();
    }
}
