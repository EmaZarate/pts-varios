using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Tasks.DeleteTask
{
    public interface IDeleteTaskUseCase
    {
        void Execute(int id);
    }
}
