using Hoshin.Quality.Domain.CorrectiveAction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.Repositories
{
    public interface ICorrectiveActionEvidenceRepository 
    {
        bool Update(int correctiveActionId, List<string> addUrls, List<string> deleteUrls);
        //bool EffectiveEvaluate(CorrectiveAction correctiveAction, List<string> addUrls);
        //bool NoEffectiveEvaluate(CorrectiveAction correctiveAction, List<string> addUrls);
    }
}
