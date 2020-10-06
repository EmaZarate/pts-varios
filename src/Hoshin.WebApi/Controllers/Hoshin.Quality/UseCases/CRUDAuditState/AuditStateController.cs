using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.AuditState.CreateAuditState;
using Hoshin.Quality.Application.UseCases.AuditState.GetAllAuditState;
using Hoshin.Quality.Application.UseCases.AuditState.GetOneAuditState;
using Hoshin.Quality.Application.UseCases.AuditState.UpdateAuditState;
using Hoshin.Quality.Domain.AuditState;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuditState = Hoshin.Quality.Domain.AuditState.AuditState;
using AuditStateClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.AuditStates;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDAuditState
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class AuditStateController : ControllerBase
    {
        private readonly IGetAllAuditStateUseCase _getAllAuditStateUseCase;
        private readonly IGetOneAuditStateUseCase _getOneAuditStateUseCase;
        private readonly ICreateAuditStateUseCase _createAuditStateUseCase;
        private readonly IUpdateAuditStateUseCase _updateAuditStateUseCase;
        private readonly IMapper _mapper;

        public AuditStateController(
                IGetAllAuditStateUseCase getAllAuditStateUseCase,
                IGetOneAuditStateUseCase getOneAuditStateUseCase,
                ICreateAuditStateUseCase createAuditStateUseCase,
                IUpdateAuditStateUseCase updateAuditStateUseCase,
                IMapper mapper
            )
        {
            _getAllAuditStateUseCase = getAllAuditStateUseCase;
            _getOneAuditStateUseCase = getOneAuditStateUseCase;
            _createAuditStateUseCase = createAuditStateUseCase;
            _updateAuditStateUseCase = updateAuditStateUseCase;
            _mapper = mapper;
        }



        [HttpGet]
        [Authorize(Policy = AuditStateClaim.ReadAuditState)]
       // [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllAuditStateUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = AuditStateClaim.ReadAuditState)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneAuditStateUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = AuditStateClaim.AddAuditState)]
        public IActionResult Add([FromBody] AuditStateDTO model)
        {
             
            return new OkObjectResult(_createAuditStateUseCase.Execute(_mapper.Map<AuditStateDTO, AuditState>(model)));
        }

        [HttpPut]
        [Authorize(Policy = AuditStateClaim.EditAuditState)]
        public IActionResult Update([FromBody] AuditStateDTO model)
        {
            return new OkObjectResult(_updateAuditStateUseCase.Execute(_mapper.Map<AuditStateDTO, AuditState>(model)));
        }



    }
}