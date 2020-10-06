using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetOneCorrectiveActionState
{
    public interface IGetOneCorrectiveActionStateUseCase
    {
        CorrectiveActionStateOutput Execute(int id);
    }
}
