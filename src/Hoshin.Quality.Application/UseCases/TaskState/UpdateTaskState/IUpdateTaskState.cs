using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.TaskState.UpdateTaskState
{
    public interface IUpdateTaskStatesUseCase
    {
        TaskStateOutput Execute(int id, string code, string name, string colour, bool active);
    }
}
