using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface ICorrectiveActionRepository
    {
        CorrectiveActionWorkflowData Add(CorrectiveActionWorkflowData correctiveAction);
        void Update(CorrectiveActionWorkflowData correctiveAction);
        CorrectiveActionWorkflowData GetOneByWorkflowId(string workflowId);
        void ChangeTasksState(int correctiveActionId, int taskNewStateID);
        List<string> GetEmailOfTasksResposibles(int correctiveActionId);
        
    }
}
