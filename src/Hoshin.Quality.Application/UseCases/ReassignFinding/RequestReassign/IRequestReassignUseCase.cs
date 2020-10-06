using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ReassignFinding.RequestReassign
{
    public interface IRequestReassignUseCase
    {
        ReassignmentsFindingHistoryOutput Execute(int findingID, string reassignedUserId, string createByUserId);
        Task<bool> Execute(FindingWorkflowData finding);
    }
}
