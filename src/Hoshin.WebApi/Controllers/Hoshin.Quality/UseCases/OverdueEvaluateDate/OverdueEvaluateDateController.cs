using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.OverdueEvaluateDateCorrectiveAction;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.OverdueEvaluateDate
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OverdueEvaluateDateController : ControllerBase
    {
        private readonly IOverdueEvaluateDateUseCase _OverdueEvaluateDateUseCase;
        private readonly IMapper _mapper;


        public OverdueEvaluateDateController(
            IOverdueEvaluateDateUseCase OverdueEvaluateDateUseCase,
            IMapper mapper)
        {
            _OverdueEvaluateDateUseCase = OverdueEvaluateDateUseCase;
            _mapper = mapper;
        }


        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.RequestEvaluateDueDateExtention)]
        public IActionResult OverdueEvaluateDate([FromBody] CorrectiveActionDTO model)
        {
            _OverdueEvaluateDateUseCase.ExecuteAsync(_mapper.Map<CorrectiveActionDTO, CorrectiveAction>(model), model.Observation, model.OverdueTime, model.CorrectiveActionID);
            return new OkObjectResult(true);
        }
    }
}