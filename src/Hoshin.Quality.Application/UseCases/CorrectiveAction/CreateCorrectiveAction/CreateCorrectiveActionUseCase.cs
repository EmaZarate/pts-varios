using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.CreateCorrectiveAction
{
    public class CreateCorrectiveActionUseCase : ICreateCorrectiveActionUseCase
    {
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly IMapper _mapper;
        private readonly IWorkflowCore _workflowCore;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateCorrectiveActionUseCase(
            ICorrectiveActionRepository correctiveActionRepository, 
            IMapper mapper, 
            IWorkflowCore workflowCore,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _correctiveActionRepository = correctiveActionRepository;
            _mapper = mapper;
            _workflowCore = workflowCore;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Execute(CorrectiveActionWorkflowData correctiveAction)
        {
            correctiveAction.Flow = "CorrectiveAction";
            correctiveAction.FlowVersion = 1;
            correctiveAction.EmitterUserID = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var flowId = await _workflowCore.StartFlow(correctiveAction);

            //var newCorrectiveAction = _correctiveActionRepository.Add(correctiveAction);
            //return _mapper.Map<Domain.CorrectiveAction.CorrectiveAction, CorrectiveActionOutput>(newCorrectiveAction);
        }
    }
}
