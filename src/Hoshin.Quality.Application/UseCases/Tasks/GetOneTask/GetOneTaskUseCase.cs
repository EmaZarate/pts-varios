using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Tasks.GetOneTask
{
    public class GetOneTaskUseCase : IGetOneTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public GetOneTaskUseCase(ITaskRepository taskRepository, IMapper mapper)
        {
            this._taskRepository = taskRepository;
            this._mapper = mapper;
        }
        public TaskOutput Execute(int id)
        {
            var task = _taskRepository.Get(id);
            if (task != null)
            {
                return _mapper.Map<Task, TaskOutput>(task);
            }
            throw new EntityNotFoundException(Convert.ToString(id), "No se encontro una tarea con ese ID");
        }
    }
}
