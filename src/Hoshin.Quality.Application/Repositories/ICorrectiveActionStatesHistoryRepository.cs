using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface ICorrectiveActionStatesHistoryRepository
    {
        int GetPreviousState(int correcticeActionID, int actualCorrectiveActionStateId);
        bool Add(int corretiveActionId, int correctiveActionStateId, string createdByUserId);
    }
}
