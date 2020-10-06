using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface ITaskRepository
    {
        List<string> GetAllResponsibleUserEmailForCorrectiveAction(int correctiveActionId);
        List<TaskWorkflowData> GetAllForCorrectiveActionWorkflow(int correctiveActionId);
    }
}
