using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.EvaluateCorrectiveAction;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EvaluateCorrectiveAction
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class EvaluateCorrectiveActionController : ControllerBase
    {
        private readonly IEvaluateCorrectiveActionUseCase _evaluateCorrectiveActionUseCase;
        private readonly IMapper _mapper;

        public EvaluateCorrectiveActionController(IEvaluateCorrectiveActionUseCase evaluateCorrectiveActionUseCase, IMapper mapper)
        {
            _evaluateCorrectiveActionUseCase = evaluateCorrectiveActionUseCase;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Evaluate)]
        public async Task<IActionResult> Evaluate([FromForm]CorrectiveActionDTO correctiveAction)
        {
            await _evaluateCorrectiveActionUseCase.Execute(_mapper.Map<CorrectiveActionDTO, CorrectiveAction>(correctiveAction), correctiveAction.CorrectiveActionEvidences);
            return new OkObjectResult(true);
        }
    }
}