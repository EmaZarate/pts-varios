using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.ExtendDueDateTask;
using Hoshin.Quality.Domain.Task;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDTask;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasksClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ExtendDueDateTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtendDueDateTaskController : ControllerBase
    {
        private readonly IExtendDueDateTaskUseCase _extendDueDateTaskUseCase;
        private readonly IMapper _mapper;

        public ExtendDueDateTaskController(
            IExtendDueDateTaskUseCase extendDueDateTaskUseCase,
            IMapper mapper)
        {
            _extendDueDateTaskUseCase = extendDueDateTaskUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = TasksClaim.ExtendDueDate)]
        public IActionResult ExtendDueDateTask([FromBody] TaskDTO editExpirationDateTaskDTO)
        {
            return new OkObjectResult(_extendDueDateTaskUseCase.Execute(_mapper.Map<TaskDTO, Task>(editExpirationDateTaskDTO)));
        }
    }
}