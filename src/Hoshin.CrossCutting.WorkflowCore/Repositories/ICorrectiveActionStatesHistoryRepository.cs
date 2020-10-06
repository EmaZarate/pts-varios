using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface ICorrectiveActionStatesHistoryRepository
    {
        bool Add(int corretiveActionId, int correctiveActionStateId, string createdByUserId);
    }
}
