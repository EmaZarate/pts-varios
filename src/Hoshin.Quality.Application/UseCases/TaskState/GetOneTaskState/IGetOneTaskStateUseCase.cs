using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.TaskState.GetOneTaskState
{
    public interface IGetOneTaskStateUseCase
    {
        TaskStateOutput Execute(int id);
    }
}
