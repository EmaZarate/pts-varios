using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.CreateCorrectiveActionState;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetAllCorrectiveActionStates;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetOneCorrectiveActionState;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.UpdateCorrectiveActionState;
using Hoshin.Quality.Domain.CorrectiveActionState;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionStateClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActionState;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveActionStates
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class CorrectiveActionStateController : ControllerBase
    {
        private readonly IGetAllCorrectiveActionStatesUseCase _getAllCorrectiveActionStatesUseCase;
        private readonly IGetOneCorrectiveActionStateUseCase _getOneCorrectiveActionStateUseCase;
        private readonly ICreateCorrectiveActionStateUseCase _createCorrectiveActionStateUseCase;
        private readonly IUpdateCorrectiveActionStateUseCase _updateCorrectiveActionStateUseCase;
        private readonly IMapper _mapper;

        public CorrectiveActionStateController(
                IGetAllCorrectiveActionStatesUseCase getAllCorrectiveActionStatesUseCase,
                IGetOneCorrectiveActionStateUseCase getOneCorrectiveActionStateUseCase,
                ICreateCorrectiveActionStateUseCase createCorrectiveActionStateUseCase,
                IUpdateCorrectiveActionStateUseCase updateCorrectiveActionStateUseCase,
                IMapper mapper
            )
        {
            _getAllCorrectiveActionStatesUseCase = getAllCorrectiveActionStatesUseCase;
            _getOneCorrectiveActionStateUseCase = getOneCorrectiveActionStateUseCase;
            _createCorrectiveActionStateUseCase = createCorrectiveActionStateUseCase;
            _updateCorrectiveActionStateUseCase = updateCorrectiveActionStateUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = CorrectiveActionStateClaim.ReadCorrectiveActionState)]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllCorrectiveActionStatesUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = CorrectiveActionStateClaim.ReadCorrectiveActionState)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneCorrectiveActionStateUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionStateClaim.AddCorrectiveActionState)]
        public IActionResult Add([FromBody] CorrectiveActionStateDTO model)
        {
            return new OkObjectResult(_createCorrectiveActionStateUseCase.Execute(_mapper.Map<CorrectiveActionStateDTO, CorrectiveActionState>(model)));
        }

        [HttpPut]
        [Authorize(Policy = CorrectiveActionStateClaim.EditCorrectiveActionState)]
        public IActionResult Update([FromBody] CorrectiveActionStateDTO model)
        {
            return new OkObjectResult(_updateCorrectiveActionStateUseCase.Execute(_mapper.Map<CorrectiveActionStateDTO, CorrectiveActionState>(model)));
        }
    }
}