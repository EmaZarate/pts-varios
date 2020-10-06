using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
//using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;
//using Microsoft.AspNetCore.SignalR;

namespace Hoshin.Quality.Application.UseCases.ReassignFinding.ApproveReassignment
{
    public class ApproveorRejectReassignment : IApproveorRejectReassignment
    {
        private readonly IReassignmentsFindingHistoryRepository _reassignmentsFindingHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IWorkflowCore _workflowCore;
        private readonly IFindingRepository _findingRepository;
        //private IHubContext<FindingsHub> _hub;
        public ApproveorRejectReassignment(
            IFindingRepository findingRepository,
            IWorkflowCore workflowCore,
            IReassignmentsFindingHistoryRepository reassignmentFindingHistoryRepository,
            IMapper mapper
            //IHubContext<FindingsHub> hub
            )
        {
            _findingRepository = findingRepository;
            _reassignmentsFindingHistoryRepository = reassignmentFindingHistoryRepository;
            _mapper = mapper;
            _workflowCore = workflowCore;
            //_hub = hub;
        }
        public ReassignmentsFindingHistoryOutput Execute(int id_findingReassignmentHistory, string state, string causeOfReject,string id_user)
        {
            throw new NotImplementedException();
           // return _mapper.Map<ReassignmentsFindingHistory, ReassignmentsFindingHistoryOutput>(_reassignmentsFindingHistoryRepository.ApporveorRejectReassignment(id_findingReassignmentHistory, state, causeOfReject, id_user));
        }

        public bool Execute(FindingWorkflowData finding)
        {
            var findingWorkflow = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, true);
            _workflowCore.ExecuteEvent("Close", finding.WorkflowId, finding);
            //_hub.Clients.All.SendAsync("transferfindingsdata", findingWorkflow);
            return true;
        }
    }
}
