using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.Quality.Application.UseCases.ApproveRejectAudit;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveRejectAudit
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]    
    public class ApproveRejectAuditController : ControllerBase
    {
        private readonly IApproveRejectAuditUseCase _approveRejectAuditUseCase;
        private readonly IMapper _mapper;
        public ApproveRejectAuditController(IApproveRejectAuditUseCase approveRejectAuditUseCase, IMapper mapper)
        {
            _approveRejectAuditUseCase = approveRejectAuditUseCase;
            _mapper = mapper;
        }

        [HttpPut]
        [Authorize(Policy = Audits.ApporvePlanning)]
        public async Task<IActionResult> ApproveRejectAudit([FromBody]ApproveRejectAuditDTO approveAudit)
        {
            await _approveRejectAuditUseCase.Execute(_mapper.Map<ApproveRejectAuditDTO, AuditWorkflowData>(approveAudit));
            return new OkObjectResult(true);
        }
    }
}
