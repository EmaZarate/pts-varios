using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.AddFindingToAspect;
using Hoshin.Quality.Application.UseCases.AuditStandardAspect.GetAllAuditStandardAspect;
using Hoshin.Quality.Application.UseCases.AuditStandardAspect.SetNoAudited;
using Hoshin.Quality.Application.UseCases.AuditStandardAspect.SetWithoutFindings;
using Hoshin.Quality.Application.UseCases.DeleteFindingFromAspect;
using Hoshin.Quality.Domain.Finding;
using Entities = Hoshin.Quality.Domain;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hoshin.CrossCutting.Authorization.Claims.Quality;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.AuditStandardAspect
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditStandardAspectController : ControllerBase
    {
        private readonly IGetAllAuditStandardAspectUseCase _getAllAuditStandardAspectUseCase;
        private readonly IAddFindingToAspectUseCase _addFindingToAspectUseCase;
        private readonly IDeleteFindingFromAspectUseCase _deleteFindingFromAspectUseCase;
        private readonly ISetWithoutFindingsAuditStandardAspectUseCase _setWithoutFindingsAuditStandardAspectUseCase;
        private readonly ISetNoAuditedAuditStandardAspectUseCase _setNoAuditedAuditStandardAspectUseCase;
        private readonly IMapper _mapper;

        public AuditStandardAspectController(
            IGetAllAuditStandardAspectUseCase getAllAuditStandardAspectUseCase,
            IAddFindingToAspectUseCase addFindingToAspectUseCase,
            IDeleteFindingFromAspectUseCase deleteFindingFromAspectUseCase,
            ISetWithoutFindingsAuditStandardAspectUseCase setWithoutFindingsAuditStandardAspectUseCase,
            ISetNoAuditedAuditStandardAspectUseCase setNoAuditedAuditStandardAspectUseCase,
            IMapper mapper
            )
        {
            _getAllAuditStandardAspectUseCase = getAllAuditStandardAspectUseCase;
            _addFindingToAspectUseCase = addFindingToAspectUseCase;
            _deleteFindingFromAspectUseCase = deleteFindingFromAspectUseCase;
            _setWithoutFindingsAuditStandardAspectUseCase = setWithoutFindingsAuditStandardAspectUseCase;
            _setNoAuditedAuditStandardAspectUseCase = setNoAuditedAuditStandardAspectUseCase;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Audits.Planning)]
      //  [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult GetAllforAudit(int id)
        {
            return new OkObjectResult(_getAllAuditStandardAspectUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = Audits.AddFindings)]
        public IActionResult AddFinding(AuditStandardAspectFindingDTO auditStandardAspectFindingDTO)
        {
            return new OkObjectResult(_addFindingToAspectUseCase.Execute(_mapper.Map<AuditStandardAspectFindingDTO, Finding>(auditStandardAspectFindingDTO)));
        }


        [HttpDelete("{FindingID}")]
        [Authorize(Policy = Audits.AddFindings)]
        public IActionResult DeleteFinding(int FindingID)
        {
            return new OkObjectResult(_deleteFindingFromAspectUseCase.Execute(FindingID));
        }

        [HttpPost]
        [Authorize(Policy = Audits.AddFindings)]
        public IActionResult SetWithoutFindings([FromBody]AuditStandardAspectDTO auditStandardAspectFindingDTO)
        {
            return new OkObjectResult(
                _setWithoutFindingsAuditStandardAspectUseCase
                    .Execute(_mapper.Map<AuditStandardAspectDTO, Entities.AuditStandardAspect>(auditStandardAspectFindingDTO)));
        }

        [HttpPost]
        [Authorize(Policy = Audits.AddFindings)]
        public IActionResult SetNoAudited([FromBody]AuditStandardAspectDTO auditStandardAspectFindingDTO)
        {
            return new OkObjectResult(
                _setNoAuditedAuditStandardAspectUseCase
                    .Execute(_mapper.Map<AuditStandardAspectDTO, Entities.AuditStandardAspect>(auditStandardAspectFindingDTO)));
        }
    }
}