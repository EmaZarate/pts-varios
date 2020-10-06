using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.Quality.Application.UseCases.GenerateCorrectiveAction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.GenerateCorrectiveAction
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateCorrectiveActionController : ControllerBase
    {
        private readonly IGenerateCorrectiveActionUseCase _generateCorrectiveActionUseCase;
        private readonly IMapper _mapper;

        public GenerateCorrectiveActionController(IGenerateCorrectiveActionUseCase generateCorrectiveActionUseCase, IMapper mapper)
        {
            _generateCorrectiveActionUseCase = generateCorrectiveActionUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Planning)]
        public async Task<IActionResult> GenerateAC([FromBody] ActionPlanDTO model)
        {
            await _generateCorrectiveActionUseCase.Execute(_mapper.Map<CorrectiveActionWorkflowData>(model));

            return new OkObjectResult(true);
        }
    }
}