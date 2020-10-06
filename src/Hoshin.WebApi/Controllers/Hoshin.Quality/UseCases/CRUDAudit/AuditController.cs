using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.Audit.CreateAudit;
using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.Quality.Application.UseCases.Audit.GetCountAudit;

using Hoshin.Quality.Application.UseCases.Audit.GetAllAudit;
using Hoshin.Quality.Application.UseCases.Audit.GetOneAudit;
using Hoshin.Quality.Application.UseCases.Audit.UpdateAudit;
using Hoshin.Quality.Application.UseCases.Audit.PlanningAudit;
using AutoMapper;
using Hoshin.Quality.Domain.Audit;
using Hoshin.Quality.Application.UseCases.Audit.EmitReportAudit;
using Hoshin.Quality.Application.UseCases.Audit.DeleteAudit;
using Audit = Hoshin.Quality.Domain.Audit.Audit;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDAudit
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class AuditController : ControllerBase
    {
        private readonly ICreateAuditUseCase _createAuditUseCase;
        private readonly IMapper _mapper;
        private readonly IGetAllAuditUseCase _getAllAuditUseCase;
        private readonly IGetOneAuditUseCase _getOneAuditUseCase;
        private readonly IUpdateAuditUseCase _updateAuditUseCase;
        private readonly IPlanningAuditUseCase _planningAuditUseCase;
        private readonly IEmitReportAuditUseCase _emitReportAuditUseCase;
        private readonly IDeleteAuditUseCase _deleteAuditUseCase;
        private readonly IGetCountAuditUseCase _getCountAuditUseCase;

        public AuditController(
            ICreateAuditUseCase createAuditUseCase,
         IGetAllAuditUseCase getAllAuditUseCase,
        IGetOneAuditUseCase getOneAuditUseCase,
        IUpdateAuditUseCase updateAuditUseCase,
        IPlanningAuditUseCase planningAuditUseCase,
        IEmitReportAuditUseCase emitReportAuditUseCase,
        IDeleteAuditUseCase deleteAuditUseCase,
        IGetCountAuditUseCase getCountAuditUseCase,
        IMapper mapper)
        {
            _createAuditUseCase = createAuditUseCase;
            _planningAuditUseCase = planningAuditUseCase;
            _emitReportAuditUseCase = emitReportAuditUseCase;
            _deleteAuditUseCase = deleteAuditUseCase;
            _getAllAuditUseCase = getAllAuditUseCase;
            _getOneAuditUseCase = getOneAuditUseCase;
            _updateAuditUseCase = updateAuditUseCase;
            _getCountAuditUseCase = getCountAuditUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = Audits.Schedule)]
        public async Task<IActionResult> Add([FromBody] AuditDTO aspectStatesDTO)
        {
            return new OkObjectResult(await _createAuditUseCase.Execute(_mapper.Map<AuditDTO, AuditWorkflowData>(aspectStatesDTO)));
        }

        /// <summary>
        /// Get audits by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = Audits.ReedAudit)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneAuditUseCase.Execute(id));
        }

        [HttpGet]
        public IActionResult GetCount()
        {
            return new OkObjectResult(_getCountAuditUseCase.Execute());
        }


        /// <summary>
        /// Get all audits.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Audits.ReedAudit)]
       // [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {

            return new OkObjectResult(_getAllAuditUseCase.Execute());
        }

        [HttpPut]
        [Authorize(Policy = Audits.Reschedule)]
        public IActionResult Update([FromBody] AuditDTO updateAuditDTO)
        {
            return new OkObjectResult(_updateAuditUseCase.Execute(_mapper.Map<AuditDTO, Audit>(updateAuditDTO)));
        }

        [HttpPost]
        [Authorize(Policy = Audits.Planning)]
        public async Task<IActionResult> Planning([FromBody] AuditDTO plannigAuditDTO)
        {
            return new OkObjectResult(await _planningAuditUseCase.Execute(_mapper.Map<AuditDTO, AuditWorkflowData>(plannigAuditDTO)));
        }

        [HttpPost]
        [Authorize(Policy = Audits.EmmitReport)]
        public async Task<IActionResult> EmitReport([FromBody] AuditDTO emitReportDTO)
        {
            return new OkObjectResult(await _emitReportAuditUseCase.Execute(_mapper.Map<AuditDTO, AuditWorkflowData>(emitReportDTO)));
        }

        [HttpPost]
        [Authorize(Policy = Audits.Delete)]
        public async Task<IActionResult> Delete([FromBody] AuditDTO deleteAuditDTO)
        {
            return new OkObjectResult(await _deleteAuditUseCase.Execute(deleteAuditDTO.AuditID, deleteAuditDTO.WorkflowId));
        }
    }
}