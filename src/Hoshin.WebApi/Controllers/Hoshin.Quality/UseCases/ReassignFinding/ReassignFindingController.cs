using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hoshin.Quality.Application.UseCases.ReassignFinding.RequestReassign;
using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.Quality.Application.UseCases.ReassignFinding.GetLastReassignment;
using Hoshin.Quality.Application.UseCases.ReassignFinding.ApproveReassignment;
using Microsoft.AspNetCore.Authorization;
using Hoshin.CrossCutting.Authorization.Claims.Quality;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ReassignFinding
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class ReassignFindingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRequestReassignUseCase _requestReassignUsecase;
        private readonly IGetLastReassignment _getLastReassignment;
        private readonly IApproveorRejectReassignment _approveorRejectReassignment;
        public ReassignFindingController(IRequestReassignUseCase requestReassignUsecase, IGetLastReassignment getLastReassignment, IApproveorRejectReassignment approveorRejectReassignment, IMapper mapper)
        {
            _requestReassignUsecase = requestReassignUsecase;
            _getLastReassignment = getLastReassignment;
            _requestReassignUsecase = requestReassignUsecase;
            _mapper = mapper;
            _approveorRejectReassignment = approveorRejectReassignment;
        }
        [HttpPost]
        [Authorize(Policy = Findings.Reassign)]
        public async Task<IActionResult> RequestReassign([FromBody] ReassignmentsFindingHistoryDTO reassignmentsFindingHistory)
        {
            return new OkObjectResult(await _requestReassignUsecase.Execute(_mapper.Map<ReassignmentsFindingHistoryDTO, FindingWorkflowData>(reassignmentsFindingHistory)));
            //return new OkObjectResult(_requestReassignUsecase.Execute(reassignmentsFindingHistory.FindingID, reassignmentsFindingHistory.ReassignedUserID, reassignmentsFindingHistory.CreatedByUserID));
        }

        [HttpGet("{id}")]

        public ActionResult getlast(int id)
        {
            return new OkObjectResult(_getLastReassignment.Execute(id));
           
        }

        //[HttpPost("{id}")]
        //public ActionResult ApproveorRejectReassignment([FromBody] ReassignmentsFindingHistoryDTO reassignmentsFindingHistory, string id)
        //{
        //    return new OkObjectResult(_approveorRejectReassignment.Execute(reassignmentsFindingHistory.Id, reassignmentsFindingHistory.State, reassignmentsFindingHistory.RejectComment, id));
        //}

        [HttpPost("{id}")]
        [Authorize(Policy = Findings.RejectReassignment)]
        [Authorize(Policy = Findings.ApproveReassignment)]
        public ActionResult ApproveorRejectReassignment([FromBody] ReassignmentsFindingHistoryDTO reassignmentsFindingHistory, string id)
        {
            return new OkObjectResult(_approveorRejectReassignment.Execute(_mapper.Map<ReassignmentsFindingHistoryDTO, FindingWorkflowData>(reassignmentsFindingHistory)));
      //      return new OkObjectResult(_approveorRejectReassignment.Execute(reassignmentsFindingHistory.Id, reassignmentsFindingHistory.State, reassignmentsFindingHistory.RejectComment, id));
        }
    }
}