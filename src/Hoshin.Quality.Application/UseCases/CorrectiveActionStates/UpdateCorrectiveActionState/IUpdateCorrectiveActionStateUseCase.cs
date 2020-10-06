using Hoshin.Quality.Domain.CorrectiveActionState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionStates.UpdateCorrectiveActionState
{
    public interface IUpdateCorrectiveActionStateUseCase
    {
        CorrectiveActionStateOutput Execute(CorrectiveActionState updateCorrectiveActionState);
    }
}
