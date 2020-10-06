using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.EditImpactCorrectiveAction
{
    public class EditImpactCorrectiveActionUseCase : IEditImpactCorrectiveActionUseCase
    {
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        public EditImpactCorrectiveActionUseCase(ICorrectiveActionRepository correctiveActionRepository)
        {
            _correctiveActionRepository = correctiveActionRepository;
        }
        public void Execute(string impact, DateTime EffectiveDateImplementation, DateTime MaxDateEfficiencyEvaluation, int correctiveActionID)
        { 
            _correctiveActionRepository.EditImpact( impact,  EffectiveDateImplementation,  MaxDateEfficiencyEvaluation,  correctiveActionID);
        }
    }
}
