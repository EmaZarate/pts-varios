using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Tasks.GetOneTask
{
    public interface IGetOneTaskUseCase
    {
        TaskOutput Execute(int id);
    }
}
