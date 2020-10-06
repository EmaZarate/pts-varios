using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.Quality.Domain.CorrectiveAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface ICorrectiveActionRepository
    {
        List<CorrectiveAction> GetAll();
        CorrectiveAction GetOne(int id);
        CorrectiveAction Update(CorrectiveAction updateCorrectiveAction);
        string GetWorkflowId(int correctiveActionId);
        void Delete(CorrectiveAction correctiveAction);
        int GetCount();
        void EditImpact(string impact, DateTime EffectiveDateImplementation, DateTime MaxDateEfficiencyEvaluation, int correctiveActionID);
        List<CorrectiveAction> GetAllFromSectorPlant(int plantId, int sectorId);
        List<CorrectiveAction> GetAllFromUser(string userId, int plantId, int sectorId);
        CorrectiveAction UpdateByReassign(CorrectiveAction correctiveAction);
    }
}
