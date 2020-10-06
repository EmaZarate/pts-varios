using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Tasks.DeleteTask
{
    public class DeleteTaskUseCase : IDeleteTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        public DeleteTaskUseCase(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }
        public void Execute(int id)
        {
            var task = _taskRepository.Get(id);
            if (task != null)
            {
                _taskRepository.Delete(task);
            }
            else
            {
                throw new EntityNotFoundException(Convert.ToString(id), "No se encontró una tarea con ese ID");
            }
        }
    }
}
