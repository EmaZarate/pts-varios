using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.ExtendDueDateCorrectiveAction
{
    public interface IExtendDueDateCorrectiveActionUseCase
    {
        bool Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction);
    }
}
