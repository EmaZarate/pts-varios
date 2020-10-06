using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.Quality.Application.UseCases.CloseFinding;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CloseFinding
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    [Authorize(Policy = Findings.Close)]
    public class CloseFindingController : ControllerBase
    {
        private readonly ICloseFindingUseCase _closeFindingUseCase;
        private readonly IMapper _mapper;

        public CloseFindingController(ICloseFindingUseCase closeFindingUseCase, IMapper mapper)
        {
            _closeFindingUseCase = closeFindingUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CloseFinding([FromBody] CloseFindingDTO model)
        {
            return new OkObjectResult(await _closeFindingUseCase.Execute(_mapper.Map<CloseFindingDTO, FindingWorkflowData>(model)));
        }
    }
}