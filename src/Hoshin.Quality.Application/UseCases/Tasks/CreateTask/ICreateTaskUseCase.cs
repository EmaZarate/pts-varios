using Hoshin.Quality.Domain.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Tasks.CreateTask
{
    public interface ICreateTaskUseCase
    {
        TaskOutput Execute(Task task);
    }
}
