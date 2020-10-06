using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.EditCorrectiveActionFishbone;
using Hoshin.Quality.Domain.CorrectiveActionFishbone;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditCorrectiveActionFishbone
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditCorrectiveActionFishboneController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEditCorrectiveActionFishboneUseCase _editCorrectiveActionFishboneUseCase;

        public EditCorrectiveActionFishboneController(IMapper mapper, IEditCorrectiveActionFishboneUseCase editCorrectiveActionFishboneUseCase)
        {
            _mapper = mapper;
            _editCorrectiveActionFishboneUseCase = editCorrectiveActionFishboneUseCase;
        }

        [HttpPost("{correctiveActionId}")]
        [Authorize(Policy = CorrectiveActionClaim.Planning)]
        public async Task<IActionResult> Post([FromBody]EditCorrectiveActionDTO model, int correctiveActionId)
       {
            await _editCorrectiveActionFishboneUseCase.Execute(_mapper.Map<List<CorrectiveActionFishbone>>(model.correctiveActionFishbone), model.RootReason, correctiveActionId);
            return new OkObjectResult(true);
        }
    }
}