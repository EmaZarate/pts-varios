using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.Quality.Application.UseCases.UpdateApprovedFinding;
using Hoshin.Quality.Domain.Evidence;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.UpdateApprovedFinding
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class UpdateApprovedFindingController : ControllerBase
    {
        private readonly IUpdateApprovedFindingUseCase _updateApprovedFindingUseCase;
        private readonly IMapper _mapper;
        public UpdateApprovedFindingController(IUpdateApprovedFindingUseCase updateApprovedFindingUseCase, IMapper mapper)
        {
            _updateApprovedFindingUseCase = updateApprovedFindingUseCase;
            _mapper = mapper;
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        [Authorize(Policy = Findings.UpdateApproved)]
        public async Task<IActionResult> UpdateApprovedFinding([FromForm] UpdateApprovedFindingDTO finding)
        {
            return new OkObjectResult(await _updateApprovedFindingUseCase.Execute(_mapper.Map<UpdateApprovedFindingDTO, FindingWorkflowData>(finding), finding.FindingEvidences, finding.FileNamesToDelete.ToList()));
        }
    }
}