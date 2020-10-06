using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Domain.CorrectiveAction;

namespace Hoshin.Quality.Application.UseCases.OverdueEvaluateDateCorrectiveAction
{
    public interface IOverdueEvaluateDateUseCase
    {
        void ExecuteAsync(Domain.CorrectiveAction.CorrectiveAction correctiveAction, string observation, DateTime overdueTime, int correctiveActionID);
    }
}
