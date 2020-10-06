using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.FindingType.CreateFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.DeleteFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetAllActiveFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetAllFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetAllForAuditFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetOneFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.UpdateFindingType;
using Hoshin.Quality.Domain.FindingType;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFindingTypes
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class FindingTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICreateFindingTypeUseCase _createFindingTypeUseCase;
        private readonly IGetAllFindingTypeUseCase _getAllFindingTypeUseCase;
        private readonly IGetAllActiveFindingTypeUseCase _getAllActiveFindingTypeUseCase;
        private readonly IGetOneFindingTypeUseCase _getOneFindingTypeUseCase;
        private readonly IGetAllForAuditFindingTypeUseCase _getAllForAuditFindingTypeUseCase;
        private readonly IUpdateFindingTypeUseCase _updateFindingTypeUseCase;
        private readonly IDeleteFindingTypeUseCase _deleteFindingTypeUseCase;
        public FindingTypesController(
            IMapper mapper,
            ICreateFindingTypeUseCase createFindingTypeUseCase, 
            IGetAllFindingTypeUseCase getAllFindingTypeUseCase,
            IGetAllActiveFindingTypeUseCase getAllActiveFindingTypeUseCase,
            IGetOneFindingTypeUseCase getOneFindingTypeUseCase,
            IUpdateFindingTypeUseCase updateFindingTypeUseCase,
            IDeleteFindingTypeUseCase deleteFindingTypeUseCase,
            IGetAllForAuditFindingTypeUseCase getAllForAuditFindingTypeUseCase
            )
        {
            _mapper = mapper;
            _createFindingTypeUseCase = createFindingTypeUseCase;
            _getAllFindingTypeUseCase = getAllFindingTypeUseCase;
            _getAllActiveFindingTypeUseCase = getAllActiveFindingTypeUseCase;
            _getOneFindingTypeUseCase = getOneFindingTypeUseCase;
            _updateFindingTypeUseCase = updateFindingTypeUseCase;
            _deleteFindingTypeUseCase = deleteFindingTypeUseCase;
            _getAllActiveFindingTypeUseCase = getAllActiveFindingTypeUseCase;
            _getAllForAuditFindingTypeUseCase = getAllForAuditFindingTypeUseCase;
        }
        [HttpPost]
        [Authorize(Policy = FindingTypes.AddTypes)]
        public IActionResult Add([FromBody] FindingTypesDTO findingType)
        {
            return new OkObjectResult(_createFindingTypeUseCase.Execute(findingType.Name, findingType.Code, findingType.Active, findingType.Parametrizations));
        }
        [HttpGet]
        [Authorize(Policy = FindingTypes.ReadConfigureTypes)]
        [Authorize(Policy = FindingTypes.ReadTypes)]
      //  [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllFindingTypeUseCase.Execute());
        }

        [HttpGet]
        [Authorize(Policy = FindingTypes.ReadConfigureTypes)]
        [Authorize(Policy = FindingTypes.ReadTypes)]
      //  [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult GetActives()
        {
            return new OkObjectResult(_getAllActiveFindingTypeUseCase.Execute());
        }

        [HttpGet]
        public IActionResult GetForAudit()
        {
            return new OkObjectResult(_getAllForAuditFindingTypeUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = FindingTypes.ReadConfigureTypes)]
        [Authorize(Policy = FindingTypes.ReadTypes)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneFindingTypeUseCase.Execute(id));
        }
        [HttpPut]
        [Authorize(Policy = FindingTypes.EditTypes)]
        [Authorize(Policy = FindingTypes.ConfigureTypes)]
        public IActionResult Update([FromBody] FindingTypesDTO findingType)
        {
            return new OkObjectResult(_updateFindingTypeUseCase.Execute(_mapper.Map<FindingTypesDTO, FindingType>(findingType)));
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = FindingTypes.DeactivateTypes)]
        [Authorize(Policy = FindingTypes.DeleteConfigureTypes)]
        public IActionResult Delete(int id)
        {
            return new OkObjectResult(_deleteFindingTypeUseCase.Execute(id));
        }
    }
}