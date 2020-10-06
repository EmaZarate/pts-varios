using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Tasks.GetCountTask
{
    public class GetCountTaskUseCase : IGetCountTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        public GetCountTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public int Execute()
        {
            return _taskRepository.GetCount();
        }
    }
}
