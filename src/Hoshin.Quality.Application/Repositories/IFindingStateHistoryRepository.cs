using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IFindingStateHistoryRepository
    {
        bool Add(int findingId, int findingStateId, string createdByUserId);
        int GetPreviousState(int findingID, int actualFindingStateId);
    }
}
