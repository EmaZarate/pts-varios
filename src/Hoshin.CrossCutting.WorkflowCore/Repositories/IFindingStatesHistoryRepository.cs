using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IFindingStatesHistoryRepository
    {
        bool Add(int findingId, int findingStateId, string createdByUserId);
        int GetPreviousState(int findingID, int actualFindingStateId);
    }
}
