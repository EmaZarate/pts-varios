using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Tasks.CreateTask
{
    public class CreateTaskUseCase : ICreateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskStateRepository _taskStateRepository;

        private const string TASK_STATE = "Borrador";

        public CreateTaskUseCase(ITaskRepository taskRepository, ITaskStateRepository taskStateRepository)
        {
            this._taskRepository = taskRepository;
            this._taskStateRepository = taskStateRepository;
        }
        public TaskOutput Execute(Task task)
        {
            task.TaskStateID = _taskStateRepository.GetByName(TASK_STATE).TaskStateID;
            _taskRepository.Add(task);
            return new TaskOutput();
        }
    }
}
