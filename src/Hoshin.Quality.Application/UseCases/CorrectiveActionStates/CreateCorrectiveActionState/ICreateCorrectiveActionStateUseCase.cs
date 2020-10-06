using Hoshin.Quality.Domain.CorrectiveActionState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionStates.CreateCorrectiveActionState
{
    public interface ICreateCorrectiveActionStateUseCase
    {
        CorrectiveActionStateOutput Execute(CorrectiveActionState newCorrectiveActionState);
    }
}
