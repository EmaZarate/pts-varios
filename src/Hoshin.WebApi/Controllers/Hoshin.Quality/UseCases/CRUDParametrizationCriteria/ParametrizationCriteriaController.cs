using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.CreateParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetAllParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetOneParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.UpdateParametrizationCriteria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Hoshin.CrossCutting.Authorization.Claims.Quality;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDParametrizationCriteria
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class ParametrizationCriteriaController : ControllerBase
    {
        private readonly ICreateParametrizationCriteriaUseCase _createParametrizationCriteriaUseCase;
        private readonly IGetAllParametrizationCriteriaUseCase _getAllParametrizationCriteriaUseCase;
        private readonly IGetOneParametrizationCriteriaUseCase _getOneParametrizationCriteriaUseCase;
        private readonly IUpdateParametrizationCriteriaUseCase _updateParametrizationCriteriaUseCase;

        public ParametrizationCriteriaController(
            ICreateParametrizationCriteriaUseCase createParametrizationCriteriaUseCase,
            IGetAllParametrizationCriteriaUseCase getAllParametrizationCriteriaUseCase,
            IGetOneParametrizationCriteriaUseCase getOneParametrizationCriteriaUseCase,
            IUpdateParametrizationCriteriaUseCase updateParametrizationCriteriaUseCase
            )
        {
            _createParametrizationCriteriaUseCase = createParametrizationCriteriaUseCase;
            _getAllParametrizationCriteriaUseCase = getAllParametrizationCriteriaUseCase;
            _getOneParametrizationCriteriaUseCase = getOneParametrizationCriteriaUseCase;
            _updateParametrizationCriteriaUseCase = updateParametrizationCriteriaUseCase;
        }
        [HttpPost]
        [Authorize(Policy = FindingParametrizationCriteria.AddParametrizationCriteria)]
        public IActionResult Add([FromBody] ParametrizationCriteriaDTO model)
        {
            return new OkObjectResult(_createParametrizationCriteriaUseCase.Execute(model.Name,model.DataType));
        }

        [HttpGet]
       // [ServiceFilter(typeof(CacheEndpointFilter))]
        [Authorize(Policy = FindingParametrizationCriteria.ReadParametrizationCriteria)]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllParametrizationCriteriaUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = FindingParametrizationCriteria.ReadParametrizationCriteria)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneParametrizationCriteriaUseCase.Execute(id));
        }

        [HttpPut]
        [Authorize(Policy = FindingParametrizationCriteria.ActivateParametrizationCriteria)]
        [Authorize(Policy = FindingParametrizationCriteria.DeactivateParametrizationCriteria)]
        [Authorize(Policy = FindingParametrizationCriteria.EditParametrizationCriteria)]
        public IActionResult Update([FromBody] ParametrizationCriteriaDTO model)
        {
            return new OkObjectResult(_updateParametrizationCriteriaUseCase.Execute(model.Id, model.Name, model.DataType));
        }
    }
}