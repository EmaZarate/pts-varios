using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hoshin.Quality.Application.UseCases.AspectStates.GetAllAspectStates;
using Hoshin.Quality.Application.UseCases.AspectStates.UpdateAspectStatus;
using Hoshin.Quality.Application.UseCases.AspectStates.CreateAspectState;
using Hoshin.Quality.Application.UseCases.AspectStates.GetOneAspectState;
using Hoshin.Quality.Domain.AspectStates;
using Hoshin.Quality.Application.UseCases.AspectStates;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Hoshin.CrossCutting.Authorization.Claims.Quality;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.AspectStates
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class AspectStatesController : ControllerBase
    {

        private readonly IGetAllAspectStatesUseCase _getAllAspectStatesUseCase;
        private readonly IUpdateAspectStatusUseCase _updateAspectStatesUseCase;
        private readonly ICreateAspectStateUseCase _createAspectStateUseCase;
        private readonly IGetOneAspectStateUseCase _getOneAspectStateUseCase;
        private readonly IMapper _mapper; 

        public AspectStatesController(
            IMapper mapper,
            IGetAllAspectStatesUseCase getAllAspectStatesUseCase,
            IUpdateAspectStatusUseCase updateAspectStatesUseCase,
            ICreateAspectStateUseCase createAspectStateUseCase,
            IGetOneAspectStateUseCase getOneAspectStateUseCase
            )
            
        {
            _getAllAspectStatesUseCase = getAllAspectStatesUseCase;
            _updateAspectStatesUseCase = updateAspectStatesUseCase;
            _mapper = mapper;
            _createAspectStateUseCase = createAspectStateUseCase;
            _getOneAspectStateUseCase = getOneAspectStateUseCase;
        }

        [HttpGet]
        [Authorize(Policy = AuditStates.ReadAuditState)]
      //  [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllAspectStatesUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = AuditStates.ReadAuditState)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneAspectStateUseCase.Execute(id));
        }

        [HttpPut]
        [Authorize(Policy = AuditStates.EditAuditState)]
        public IActionResult Update([FromBody] AspectStatesDTO aspectStatesDTO)
        {
            return new OkObjectResult(_updateAspectStatesUseCase.Execute(aspectStatesDTO.ID,aspectStatesDTO.Name,aspectStatesDTO.Colour,aspectStatesDTO.Active));
        }

        [HttpPost]
        [Authorize(Policy = AuditStates.AddAuditState)]
        public IActionResult Add([FromBody] AspectStatesDTO aspectStatesDTO)
        {
            return new OkObjectResult(_createAspectStateUseCase.Execute(aspectStatesDTO.Name, aspectStatesDTO.Colour, aspectStatesDTO.Active));
        }
    }
}