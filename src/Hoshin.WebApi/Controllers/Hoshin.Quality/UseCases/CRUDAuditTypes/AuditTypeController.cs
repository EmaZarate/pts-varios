using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.AddAuditTypeUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllActivesAuditTypesUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllAuditTypesUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetOneAuditTypeUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.UpdateAuditTypeUseCase;
using Hoshin.Quality.Domain.AuditType;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuditTypeClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.AuditType;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDAuditTypes
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class AuditTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAllAuditTypesUseCase _getAllAuditTypesUseCase;
        private readonly IGetAllActivesAuditTypesUseCase _getAllActivesAuditTypesUseCase;
        private readonly IGetOneAuditTypeUseCase _getOneAuditTypeUseCase;
        private readonly IAddAuditTypeUseCase _addAuditTypeUseCase;
        private readonly IUpdateAuditTypeUseCase _updateAuditTypeUseCase;

        public AuditTypeController(
            IMapper mapper,
            IGetAllAuditTypesUseCase getAllAuditTypesUseCase,
            IGetAllActivesAuditTypesUseCase getAllActivesAuditTypesUseCase,
            IGetOneAuditTypeUseCase getOneAuditTypeUseCase,
            IAddAuditTypeUseCase addAuditTypeUseCase,
            IUpdateAuditTypeUseCase updateAuditTypeUseCase
            )
        {
            _mapper = mapper;
            _getAllAuditTypesUseCase = getAllAuditTypesUseCase;
            _getAllActivesAuditTypesUseCase = getAllActivesAuditTypesUseCase;
            _getOneAuditTypeUseCase = getOneAuditTypeUseCase;
            _addAuditTypeUseCase = addAuditTypeUseCase;
            _updateAuditTypeUseCase = updateAuditTypeUseCase;
        }

        [HttpGet]
        [Authorize(Policy = AuditTypeClaim.ReadAuditType)]
       // [ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _getAllAuditTypesUseCase.Execute());
        }

        [HttpGet]
        [Authorize(Policy = AuditTypeClaim.ReadAuditType)]
        public async Task<IActionResult> GetActives()
        {
            return new OkObjectResult(await _getAllActivesAuditTypesUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = AuditTypeClaim.ReadAuditType)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneAuditTypeUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = AuditTypeClaim.AddAuditType)]
        public IActionResult Add([FromBody] AuditTypeDTO auditType)
        {
            return new OkObjectResult(_addAuditTypeUseCase.Execute(_mapper.Map<AuditTypeDTO, AuditType>(auditType)));
        }

        [HttpPut]
        [Authorize(Policy = AuditTypeClaim.EditAuditType)]
        [Authorize(Policy = AuditTypeClaim.ActivateAuditType)]
        [Authorize(Policy = AuditTypeClaim.DeactivateAuditType)]
        public IActionResult Update([FromBody] AuditTypeDTO auditType)
        {
            return new OkObjectResult(_updateAuditTypeUseCase.Execute(_mapper.Map<AuditTypeDTO, AuditType>(auditType)));
        }
    }
}