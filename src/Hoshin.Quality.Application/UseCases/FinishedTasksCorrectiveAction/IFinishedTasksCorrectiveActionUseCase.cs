using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.FinishedTasksCorrectiveAction
{
    public interface IFinishedTasksCorrectiveActionUseCase
    {
        Task Execute(int correctiveActionId);
    }
}
