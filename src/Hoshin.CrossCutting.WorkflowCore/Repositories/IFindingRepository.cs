using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IFindingRepository
    {
        FindingWorkflowData Add(FindingWorkflowData finding);
        string GetResponsibleUserEmail(string responsibleUserId);
        string GetFindingTypeName(int findingTypeId);
        FindingWorkflowData Update(FindingWorkflowData finding);
        FindingWorkflowData GetOneByWorkflowId(string workflowId);
        List<FindingWorkflowData> GetAllByAuditID(int id);
        bool setWorkflowID(int id, string workflowid);
        FindingWorkflowData UpdateIsInProcessWorkflow(int findingID, bool isInProcessWorkflow);
    }
}
