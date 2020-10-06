using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims;
//using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Application.Exceptions.Finding;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.SignalR;

namespace Hoshin.Quality.Application.UseCases.ReassignFinding.RequestReassign
{
    public class RequestReassignUseCase : IRequestReassignUseCase
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IReassignmentsFindingHistoryRepository _reassignmentsFindingHistoryRepository;
        private readonly IWorkflowCore _workflowCore;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private IHubContext<FindingsHub> _hub;

        public RequestReassignUseCase(
            IFindingRepository findingRepository, 
            IReassignmentsFindingHistoryRepository reassignmentFindingHistoryRepository, 
            IWorkflowCore workflowCore, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
            //IHubContext<FindingsHub> hub
            )
        {
            _findingRepository = findingRepository;
            _reassignmentsFindingHistoryRepository = reassignmentFindingHistoryRepository;
            _workflowCore = workflowCore;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            //_hub = hub;
        }
        public ReassignmentsFindingHistoryOutput Execute(int findingID, string reassignedUserId, string createByUserId)
        {
            ReassignmentsFindingHistory reassignmentsFindingHistory =  _reassignmentsFindingHistoryRepository.Add(new ReassignmentsFindingHistory(findingID,reassignedUserId,createByUserId, "Pendiente"));
            return _mapper.Map<ReassignmentsFindingHistory, ReassignmentsFindingHistoryOutput>(reassignmentsFindingHistory);
        }

        public async Task<bool> Execute(FindingWorkflowData findingWorkflowData)
        {
            Domain.Finding.Finding finding = _findingRepository.Get(findingWorkflowData.FindingID);
            if (findingWorkflowData.ReassignedUserID == finding.ResponsibleUserID)
            {
                throw new ReassignedUserCantBeActualResponsibleUserException(finding, findingWorkflowData.ReassignedUserID);
            }
            if(_httpContextAccessor.HttpContext.User.HasClaim(CustomClaimTypes.Permission, CrossCutting.Authorization.Claims.Quality.Findings.ReassginDirectly))
            {
                findingWorkflowData.EventData = "ReassignedWithoutApproval";
            }
            var findingWorkflow = _findingRepository.UpdateIsInProcessWorkflow(findingWorkflowData.FindingID, true);
            await _workflowCore.ExecuteEvent("Close", findingWorkflowData.WorkflowId, findingWorkflowData);
            //await _hub.Clients.All.SendAsync("transferfindingsdata", findingWorkflow);
            return true;
        }
    }
}
