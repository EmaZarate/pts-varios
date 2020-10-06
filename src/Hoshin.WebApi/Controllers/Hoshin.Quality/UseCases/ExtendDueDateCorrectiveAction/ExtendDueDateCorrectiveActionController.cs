using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.ExtendDueDateCorrectiveAction;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ExtendDueDateCorrectiveAction
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtendDueDateCorrectiveActionController : ControllerBase
    {
        private readonly IExtendDueDateCorrectiveActionUseCase _extendDueDateCorrectiveActionUseCase;
        private readonly IMapper _mapper;

        public ExtendDueDateCorrectiveActionController(
            IExtendDueDateCorrectiveActionUseCase extendDuePlannigDateCorrectiveActionUseCase,
            IMapper mapper)
        {
            _extendDueDateCorrectiveActionUseCase = extendDuePlannigDateCorrectiveActionUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.ExtendEvaluateDueDate)]
        public IActionResult ExtendDueDateCorrectiveAction([FromBody] CorrectiveActionDTO editExpirationDateFindingDTO)
        {
            return new OkObjectResult(_extendDueDateCorrectiveActionUseCase.Execute(_mapper.Map<CorrectiveActionDTO, CorrectiveAction>(editExpirationDateFindingDTO)));
        }
    }
}