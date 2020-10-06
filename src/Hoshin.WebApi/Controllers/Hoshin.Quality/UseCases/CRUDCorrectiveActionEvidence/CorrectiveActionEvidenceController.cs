using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.CorrectiveActionEvidence.UpdateCorrectiveActionEvidence;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveActionEvidence
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CorrectiveActionEvidenceController : ControllerBase
    {

        private readonly IUpdateCorrectiveActionEvidenceUseCase _updateCorrectiveActionEvidenceUseCase;
        private readonly IMapper _mapper;
        public CorrectiveActionEvidenceController(
            IUpdateCorrectiveActionEvidenceUseCase updateCorrectiveActionEvidenceUseCase,
            IMapper mapper)
        {
            _updateCorrectiveActionEvidenceUseCase = updateCorrectiveActionEvidenceUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Planning)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Update([FromForm]CorrectiveActionDTO correctiveAction)
        {    
            return new OkObjectResult(await _updateCorrectiveActionEvidenceUseCase.Execute(_mapper.Map<CorrectiveActionDTO, CorrectiveAction>(correctiveAction), correctiveAction.CorrectiveActionEvidences, correctiveAction.FileNamesToDelete.ToList()));
        }
    }
}