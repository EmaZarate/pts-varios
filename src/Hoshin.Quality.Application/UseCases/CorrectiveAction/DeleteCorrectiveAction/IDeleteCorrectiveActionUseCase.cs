using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.DeleteCorrectiveAction
{
    public interface IDeleteCorrectiveActionUseCase
    {
        void Execute(int id);
    }
}
