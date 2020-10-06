using AutoMapper;
using Hoshin.Quality.Application.UseCases.Tasks.CreateTask;
using Hoshin.Quality.Application.UseCases.Tasks.DeleteTask;
using Hoshin.Quality.Application.UseCases.Tasks.GetAllTask;
using Hoshin.Quality.Application.UseCases.Tasks.GetAllTask;
using Hoshin.Quality.Application.UseCases.Tasks.GetCountTask;
using Hoshin.Quality.Application.UseCases.Tasks.GetOneTask;
using Hoshin.Quality.Application.UseCases.Tasks.UpdateTask;
using Hoshin.Quality.Domain.Task;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskModel = Hoshin.Quality.Domain.Task;
using TasksClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.Tasks;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;
using Hoshin.Quality.Application.UseCases.Tasks.GetAllTasksForAC;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDTask
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ICreateTaskUseCase _createTaskUseCase;
        private readonly IGetAllTaskUseCase _getAllTaskUseCase;
        private readonly IGetOneTaskUseCase _getOneTaskUseCase;
        private readonly IUpdateTaskUseCase _updateTaskUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;
        private readonly IGetCountTaskUseCase _getCountTaskUseCase;
        private readonly IGetAllTasksForACUseCase _getAllTasksForACUseCase;
        private readonly IMapper _mapper;

        public TaskController(
            ICreateTaskUseCase createTaskUseCase,
            IGetAllTaskUseCase getAllTaskUseCase,
            IGetAllTasksForACUseCase getAllTasksForACUseCase,
            IGetOneTaskUseCase getOneTaskUseCase,
            IUpdateTaskUseCase updateTaskUseCase,
            IDeleteTaskUseCase deleteTaskUseCase,
            IGetCountTaskUseCase getCountTaskUseCase,
            IMapper mapper
        )
        {
            this._createTaskUseCase = createTaskUseCase;
            this._getAllTaskUseCase = getAllTaskUseCase;
            this._getOneTaskUseCase = getOneTaskUseCase;
            this._updateTaskUseCase = updateTaskUseCase;
            this._deleteTaskUseCase = deleteTaskUseCase;
            this._getCountTaskUseCase = getCountTaskUseCase;
            _getAllTasksForACUseCase = getAllTasksForACUseCase;

            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = TasksClaim.Reedtask)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneTaskUseCase.Execute(id));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = TasksClaim.Reedtask)]
        public IActionResult GetAllByCorrectiveActionID(int id)
        {
            return new OkObjectResult(_getAllTaskUseCase.Execute(id));
        }

        [HttpGet]
        [Authorize(Policy = TasksClaim.Reedtask)]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _getAllTaskUseCase.Execute());
        }

        [HttpGet]
        [Authorize(Policy = CorrectiveActionClaim.Read)]
        public IActionResult GetAllForAC()
        {
            return new OkObjectResult(_getAllTasksForACUseCase.Execute());
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Planning)]
        public IActionResult Add([FromBody] TaskDTO model)
        {
            return new ObjectResult(_createTaskUseCase.Execute(_mapper.Map<TaskDTO, TaskModel.Task>(model)));
        }

        [HttpPut]
        [Authorize(Policy = TasksClaim.EditTask)]
        public IActionResult Update([FromBody] TaskDTO model)
        {
            return new ObjectResult(_updateTaskUseCase.Execute(_mapper.Map<TaskDTO, TaskModel.Task>(model)));
        }

        [HttpPost]
        [Authorize(Policy = TasksClaim.Reedtask)]
        public IActionResult Delete([FromBody] TaskDTO model)
        {
            _deleteTaskUseCase.Execute(model.TaskID);
            return new OkObjectResult(model);
        }

        [HttpPost]
        [Authorize(Policy = TasksClaim.EditTask)]
        public async Task<IActionResult> UpdateTask([FromForm] TaskDTO model)
        {
            try
            {
                await _updateTaskUseCase.Execute(_mapper.Map<TaskDTO, TaskModel.Task>(model), model.TaskEvidences, model.DeleteEvidencesUrls.ToList());
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetCount()
        {
            return new OkObjectResult(_getCountTaskUseCase.Execute());
        }


        [HttpPost()]
        [Authorize(Policy = TasksClaim.RequestDueDateExtention)]
        public  IActionResult overdueExtend([FromBody] TaskDTO model) {

            _updateTaskUseCase.Execute(_mapper.Map<TaskDTO, TaskModel.Task>(model), model.Observation, model.overdureTime, model.TaskID);
            return new OkObjectResult(true);
        }
    }
}