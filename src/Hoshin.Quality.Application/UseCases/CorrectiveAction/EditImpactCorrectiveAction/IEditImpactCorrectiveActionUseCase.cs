using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.EditImpactCorrectiveAction
{
    public interface IEditImpactCorrectiveActionUseCase
    {
        //void Execute(string impact, int correctiveActionID);
        void Execute(string impact, DateTime MaxDateImplementation, DateTime MaxDateEfficiencyEvaluation, int correctiveActionID);
    }
}
