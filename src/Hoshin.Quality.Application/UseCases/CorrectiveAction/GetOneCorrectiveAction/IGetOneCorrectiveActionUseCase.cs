using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.GetOneCorrectiveAction
{
    public interface IGetOneCorrectiveActionUseCase
    {
        CorrectiveActionOutput Execute(int id);
    }
}
