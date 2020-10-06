using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.ReassignFinding.ApproveReassignment
{
    public interface IApproveorRejectReassignment
    {
        ReassignmentsFindingHistoryOutput Execute(int Id, string state, string causeOfReject,string id_user);
        bool Execute(FindingWorkflowData finding);
    }
}
