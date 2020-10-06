using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IReassignmentsFindingHistoryRepository
    {
        ReassignmentsFindingHistory Add(ReassignmentsFindingHistory requestReassign);
        ReassignmentsFindingHistory GetLast(int id_finding);
        
    }
}
