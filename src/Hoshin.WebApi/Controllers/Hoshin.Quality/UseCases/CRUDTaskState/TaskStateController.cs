using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.TaskState.GetAll;
using Hoshin.Quality.Application.UseCases.TaskState.UpdateTaskState;
using Hoshin.Quality.Application.UseCases.TaskState.GetOneTaskState;
using Hoshin.Quality.Application.UseCases.TaskState.CreateTaskState;
using Hoshin.WebApi.Filters;
using Hoshin.Quality.Domain.TaskState;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
//using Hoshin.CrossCutting.Authorization.Claims.Quality.TaskStates;


namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDTaskState
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class TaskStateController : Controller
    {
        private readonly IGetAllTaskStatesUseCase _getAllTaskStatesUseCase;
        private readonly IGetOneTaskStateUseCase _getOneTaskStateUseCase;
        private readonly IUpdateTaskStatesUseCase _updateTaskStateUseCase;
        private readonly ICreateTaskStateUseCase _createTaskStateUseCase;
        private readonly IMapper _mapper;

        public TaskStateController(
            IGetAllTaskStatesUseCase getAllTaskStatesUseCase,
            IUpdateTaskStatesUseCase updateTaskStateUseCase,
            IGetOneTaskStateUseCase  getOneTaskStateUseCase,
            ICreateTaskStateUseCase  createTaskStateUseCase,
            IMapper mapper)
        {
            _getAllTaskStatesUseCase = getAllTaskStatesUseCase;
            _getOneTaskStateUseCase =  getOneTaskStateUseCase;
            _updateTaskStateUseCase =  updateTaskStateUseCase;
            _createTaskStateUseCase = createTaskStateUseCase;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Policy = TaskStates.ReadTaskState)]
        //  [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllTaskStatesUseCase.Execute());
        }
        [HttpGet("{id}")]
        [Authorize(Policy = TaskStates.ReadTaskState)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneTaskStateUseCase.Execute(id));
        }

        [HttpPut]
        [Authorize(Policy = TaskStates.EditTaskState)]
        public IActionResult Update([FromBody] TaskStateDTO model)
        {
            return new OkObjectResult(_updateTaskStateUseCase.Execute(model.TaskStateID,model.Code, model.Name, model.Color,model.Active));
        }

        [HttpPost]
        [Authorize(Policy = TaskStates.AddTaskState)]
        public IActionResult Add([FromBody] TaskStateDTO model)
        {
            return new OkObjectResult(_createTaskStateUseCase.Execute(model.Active, model.Code, model.Color, model.Name));
        }


    }


}