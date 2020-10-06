using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.ReassignFinding.GetLastReassignment
{
    public interface IGetLastReassignment
    {
        ReassignmentsFindingHistoryOutput Execute(int id);   
    }
}
