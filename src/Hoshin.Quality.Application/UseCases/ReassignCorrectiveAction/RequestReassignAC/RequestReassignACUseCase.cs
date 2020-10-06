using AutoMapper;
using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.CorrectiveAction;
using Hoshin.Quality.Application.UseCases.ReassignFinding.RequestReassign;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.ReassignCorrectiveAction.RequestReassignAC
{
    public class RequestReassignACUseCase : IRequestReassignACUseCase
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IReassignmentsFindingHistoryRepository _reassignmentsFindingHistoryRepository;
        private readonly IWorkflowCore _workflowCore;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHubContext<FindingsHub> _hub;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        public RequestReassignACUseCase(
            IFindingRepository findingRepository,
            IReassignmentsFindingHistoryRepository reassignmentFindingHistoryRepository,
            IWorkflowCore workflowCore,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IHubContext<FindingsHub> hub,
            ICorrectiveActionRepository correctiveActionRepository
            )
        {
            _findingRepository = findingRepository;
            _reassignmentsFindingHistoryRepository = reassignmentFindingHistoryRepository;
            _workflowCore = workflowCore;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _hub = hub;
            _correctiveActionRepository = correctiveActionRepository;
        }

        public CorrectiveActionOutput Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction)
        {
            return _mapper.Map<Domain.CorrectiveAction.CorrectiveAction, CorrectiveActionOutput>(_correctiveActionRepository.UpdateByReassign(correctiveAction));
        }
    }
}
