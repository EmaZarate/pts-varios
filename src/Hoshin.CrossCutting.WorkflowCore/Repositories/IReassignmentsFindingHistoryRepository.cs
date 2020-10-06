using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IReassignmentsFindingHistoryRepository
    {
        bool Add(int findingID, string reassignedUserId, string createByUserId, string state, string rejectComment ="");
    }
}
