using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.Quality.Application.UseCases.ApproveRejectReportAudit;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveRejectReportAudit
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class ApproveRejectReportController : ControllerBase
    {
        private readonly IApproveRejectReportAuditUseCase _approveRejectReportAudit;
        private readonly IMapper _mapper;

        public ApproveRejectReportController(IApproveRejectReportAuditUseCase approveRejectReportAudit, IMapper mapper)
        {
            _approveRejectReportAudit = approveRejectReportAudit;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(Policy = Audits.ApporveReport)]
        public async Task<IActionResult> ApproveReject([FromBody] ApproveRejectReportDTO approveRejectReport)
        {
            return new OkObjectResult(
                await _approveRejectReportAudit.Execute(_mapper.Map<ApproveRejectReportDTO, AuditWorkflowData>(approveRejectReport))
                );
        }
        
    }
}