using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hoshin.Quality.Domain.CorrectiveAction;

namespace Hoshin.Quality.Application.UseCases.OverduePlannigDate
{
    public interface IOverduePlannignDateUseCase
    {
        void ExecuteAsync(Domain.CorrectiveAction.CorrectiveAction correctiveAction, string observation, DateTime overdureTime, int correctiveActionID);
    }
}
