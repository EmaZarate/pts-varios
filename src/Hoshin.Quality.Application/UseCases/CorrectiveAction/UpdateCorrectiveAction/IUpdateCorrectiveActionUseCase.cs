using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.UpdateCorrectiveAction
{
    public interface IUpdateCorrectiveActionUseCase
    {
        CorrectiveActionOutput Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction);
    }
}
