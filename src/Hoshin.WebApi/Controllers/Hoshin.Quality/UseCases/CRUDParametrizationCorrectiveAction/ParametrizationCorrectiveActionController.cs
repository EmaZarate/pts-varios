using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.CreateParametrizationCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetAllParametrizationCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetOneParametrizationCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.UpdateParametrizationCorrectiveAction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using CorrectiveActionParametrizationClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActionParametrization;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDParametrizationCorrectiveAction
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class ParametrizationCorrectiveActionController : ControllerBase
    {
        private readonly ICreateParametrizationCorrectiveActionUseCase _createParametrizationCorrectiveActionUseCase;
        private readonly IGetAllParametrizationCorrectiveActionUseCase _getAllParametrizationCorrectiveActionUseCase;
        private readonly IGetOneParametrizationCorrectiveActionUseCase _getOneParametrizationCorrectiveActionUseCase;
        private readonly IUpdateParametrizationCorrectiveActionUseCase _updateParametrizationCorrectiveActionUseCase;

        public ParametrizationCorrectiveActionController(
            ICreateParametrizationCorrectiveActionUseCase createParametrizationCorrectiveActionUseCase, 
            IGetAllParametrizationCorrectiveActionUseCase getAllParametrizationCorrectiveActionUseCase,
            IGetOneParametrizationCorrectiveActionUseCase getOneParametrizationCorrectiveActionUseCase,
            IUpdateParametrizationCorrectiveActionUseCase updateParametrizationCorrectiveActionUseCase
            )
        {
            _createParametrizationCorrectiveActionUseCase = createParametrizationCorrectiveActionUseCase;
            _getAllParametrizationCorrectiveActionUseCase = getAllParametrizationCorrectiveActionUseCase;
            _getOneParametrizationCorrectiveActionUseCase = getOneParametrizationCorrectiveActionUseCase;
            _updateParametrizationCorrectiveActionUseCase = updateParametrizationCorrectiveActionUseCase;
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionParametrizationClaim.AddCorrectiveActionParametrization)]
        public IActionResult Add([FromBody] ParametrizationCorrectiveActionDTO model)
        {
            return new OkObjectResult(_createParametrizationCorrectiveActionUseCase.Execute(model.Name,model.Code,model.value));
        }

        [HttpGet]
        [Authorize(Policy = CorrectiveActionParametrizationClaim.ReadCorrectiveActionParametrization)]
        //     [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {
            var x = _getAllParametrizationCorrectiveActionUseCase.Execute();
                return new OkObjectResult(x);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = CorrectiveActionParametrizationClaim.ReadCorrectiveActionParametrization)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneParametrizationCorrectiveActionUseCase.Execute(id));
        }

        [HttpPut]
        [Authorize(Policy = CorrectiveActionParametrizationClaim.EditCorrectiveActionParametrization)]
        public IActionResult Update([FromBody] ParametrizationCorrectiveActionDTO model)
        {
            return new OkObjectResult(_updateParametrizationCorrectiveActionUseCase.Execute(model.ParametrizationCorrectiveActionId, model.Name, model.Code, model.value));
        }
    }
}