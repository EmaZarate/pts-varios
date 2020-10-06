using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.Quality.Application.UseCases.CreateFinding;
using Hoshin.Quality.Domain.Evidence;
using Hoshin.WebApi.Controllers.DTO;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CreateFinding
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    [Authorize(Policy = Findings.Add)]
    public class CreateFindingController : ControllerBase
    {
        private readonly ICreateFindingUseCase _createFindingUseCase;
        private readonly IMapper _mapper;

        public CreateFindingController(ICreateFindingUseCase createFindingUseCase, IMapper mapper)
        {
            _createFindingUseCase = createFindingUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<FindingWorkflowData>> CreateFinding([FromForm]CreateFindingDTO finding)
        {
            return new OkObjectResult(await _createFindingUseCase.Execute(_mapper.Map<CreateFindingDTO, FindingWorkflowData>(finding), finding.FindingEvidences));
        }
    }
}