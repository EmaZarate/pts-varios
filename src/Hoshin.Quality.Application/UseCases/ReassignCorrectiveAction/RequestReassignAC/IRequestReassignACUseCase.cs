using Hoshin.Quality.Application.UseCases.CorrectiveAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.ReassignCorrectiveAction.RequestReassignAC
{
    public interface IRequestReassignACUseCase
    {
        CorrectiveActionOutput Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction);
    }
}
