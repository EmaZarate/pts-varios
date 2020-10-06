using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.CreateCorrectiveAction;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.DeleteCorrectiveAction;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.EditImpactCorrectiveAction;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.GetAllCorrectiveAction;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.GetCountCorrectiveActions;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.GetOneCorrectiveAction;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.UpdateCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ReassignCorrectiveAction.RequestReassignAC;
using Hoshin.Quality.Application.UseCases.ReassignFinding.RequestReassign;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;


namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class CorrectiveActionController : ControllerBase
    {
        private readonly IGetAllCorrectiveActionsUseCase _getAllCorrectiveActionsUseCase;
        private readonly ICreateCorrectiveActionUseCase _createCorrectiveActionUseCase;
        private readonly IGetOneCorrectiveActionUseCase _getOneCorrectiveActionsUseCase;
        private readonly IDeleteCorrectiveActionUseCase _deleteCorrectiveActionUseCase;
        private readonly IGetCountCorrectiveActionsUseCase _getCountCorrectiveActionsUseCase;
        private readonly IUpdateCorrectiveActionUseCase _updateCorrectiveActionUseCase;
        private readonly IEditImpactCorrectiveActionUseCase _editImpactCorrectiveActionUseCase;
        private readonly IMapper _mapper;
        private readonly IRequestReassignACUseCase _requestReassignACUseCase;

        public CorrectiveActionController(IGetAllCorrectiveActionsUseCase getAllCorrectiveActionsUseCase,
                                            ICreateCorrectiveActionUseCase createCorrectiveActionUseCase,
                                            IGetOneCorrectiveActionUseCase getOneCorrectiveActionsUseCase,
                                            IDeleteCorrectiveActionUseCase deleteCorrectiveActionUseCase,
                                            IGetCountCorrectiveActionsUseCase getCountCorrectiveActionsUseCase,
                                            IUpdateCorrectiveActionUseCase updateCorrectiveActionUseCase,
                                            IEditImpactCorrectiveActionUseCase editImpactCorrectiveActionUseCase,
                                            IRequestReassignACUseCase requestReassignACUseCase,
                                            IMapper mapper)
        {
            _getAllCorrectiveActionsUseCase = getAllCorrectiveActionsUseCase;
            _createCorrectiveActionUseCase = createCorrectiveActionUseCase;
            _getOneCorrectiveActionsUseCase = getOneCorrectiveActionsUseCase;
            _deleteCorrectiveActionUseCase = deleteCorrectiveActionUseCase;
            _getCountCorrectiveActionsUseCase = getCountCorrectiveActionsUseCase;
            _updateCorrectiveActionUseCase = updateCorrectiveActionUseCase;
            _editImpactCorrectiveActionUseCase = editImpactCorrectiveActionUseCase;
            _requestReassignACUseCase = requestReassignACUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = CorrectiveActionClaim.Read)]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _getAllCorrectiveActionsUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = CorrectiveActionClaim.Read)]
        public IActionResult GetOne(int id)
        {
            return new OkObjectResult(_getOneCorrectiveActionsUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Planning)]
        public IActionResult EditImpact(EditImpactDTO model)
        {
            _editImpactCorrectiveActionUseCase.Execute(model.Impact, model.MaxDateImplementation, model.MaxDateEfficiencyEvaluation, model.CorrectiveActionID);
            return new OkObjectResult(true);
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Schedule)]
        public async Task Add([FromBody] CorrectiveActionDTO model)
        {
            await _createCorrectiveActionUseCase.Execute(_mapper.Map<CorrectiveActionDTO, CorrectiveActionWorkflowData>(model));
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Planning)]
        public IActionResult Update([FromBody] CorrectiveActionDTO model)
        {
            return new OkObjectResult(_updateCorrectiveActionUseCase.Execute(_mapper.Map<CorrectiveActionDTO, CorrectiveAction>(model)));
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Delete)]
        public IActionResult Delete([FromBody] CorrectiveActionDTO model)
        {
            _deleteCorrectiveActionUseCase.Execute(model.CorrectiveActionID);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetCount()
        {
            return new OkObjectResult(_getCountCorrectiveActionsUseCase.Execute());
        }

        public IActionResult UpdateACReassign([FromBody] CorrectiveActionDTO model)
        {
            return new OkObjectResult(_requestReassignACUseCase.Execute(_mapper.Map<CorrectiveActionDTO, CorrectiveAction>(model)));
        }
    }
}