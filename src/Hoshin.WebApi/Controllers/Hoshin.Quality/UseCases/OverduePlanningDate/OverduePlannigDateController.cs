using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.OverduePlannigDate;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.OverduePlanningDate
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OverduePlannigDateController : ControllerBase
    {
        private readonly IOverduePlannignDateUseCase _overduePlannignDateUseCase;
        private readonly IMapper _mapper;


        public OverduePlannigDateController(
            IOverduePlannignDateUseCase overduePlannignDateUseCase,
            IMapper mapper) {
            _overduePlannignDateUseCase = overduePlannignDateUseCase;
            _mapper = mapper;
        }

       
        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.RequestPlanningDueDateExtention)]
        public IActionResult OverduePlanningDate([FromBody] CorrectiveActionDTO model)
        {
            _overduePlannignDateUseCase.ExecuteAsync(_mapper.Map<CorrectiveActionDTO, CorrectiveAction>(model), model.Observation, model.OverdueTime, model.CorrectiveActionID);
            return new OkObjectResult(true);
        }
    }
}