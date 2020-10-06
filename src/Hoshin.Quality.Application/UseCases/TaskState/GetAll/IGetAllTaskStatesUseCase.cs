using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.TaskState.GetAll
{
    public interface IGetAllTaskStatesUseCase
    {
        List<TaskStateOutput> Execute();
    }
}
