using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.ApproveFinding;
using Hoshin.Quality.Domain.Finding;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hoshin.WebApi.Filters;
using Hoshin.Quality.Domain.Evidence;
using Newtonsoft.Json;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;
using Hoshin.CrossCutting.Authorization.Claims.Quality;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveFinding
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    [Authorize(Policy = Findings.Approve)]
    public class ApproveFindingController : ControllerBase
    {
        private readonly IApproveFindingUseCase _approveFindingUseCase;
        private readonly IMapper _mapper;
        public ApproveFindingController(IApproveFindingUseCase approveFindingUseCase, IMapper mapper)
        {
            _approveFindingUseCase = approveFindingUseCase;
            _mapper = mapper;
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ApproveFinding([FromForm]ApproveFindingDTO finding)
        {          
            return new OkObjectResult(await _approveFindingUseCase.Execute(_mapper.Map<ApproveFindingDTO, FindingWorkflowData>(finding), finding.FindingEvidences, finding.FileNamesToDelete.ToList()));
        }
    }
}