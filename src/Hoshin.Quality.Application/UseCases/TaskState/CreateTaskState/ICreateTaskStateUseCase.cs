using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.TaskState.CreateTaskState
{
   public  interface ICreateTaskStateUseCase
    {
        TaskStateOutput Execute(bool active, string code, string color, string name);
    }
}
