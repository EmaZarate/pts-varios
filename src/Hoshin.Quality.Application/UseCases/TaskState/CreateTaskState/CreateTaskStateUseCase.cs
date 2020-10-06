using Hoshin.Quality.Application.UseCases.TaskState.CreateTaskState;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Core.Application.Exceptions.Common;

namespace Hoshin.Quality.Application.UseCases.TaskState.CreateTaskStateUseCase
{
    public class CreateTaskStateUseCase : ICreateTaskStateUseCase
    {
        private readonly ITaskStateRepository _taskStateRepository;

        public CreateTaskStateUseCase(ITaskStateRepository taskStateRepository)
        {
            _taskStateRepository = taskStateRepository;
        }

        public TaskStateOutput Execute(bool active, string code, string color, string name)
        {
            if (_taskStateRepository.Get(code, name, color) == null)
            {
                var newParam = new Domain.TaskState.TaskState(code, name, color, active);
                newParam = _taskStateRepository.Add(newParam);
                return new TaskStateOutput(newParam.TaskStateID, newParam.Name, newParam.Color, newParam.Active, newParam.Code);
            }
            else
            {
                throw new DuplicateEntityException(name, "Ya existe un estado de tarea con este nombre o código");
            }

        }
    }
    
}
