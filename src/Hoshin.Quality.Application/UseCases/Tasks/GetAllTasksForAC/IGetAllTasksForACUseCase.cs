using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Tasks.GetAllTasksForAC
{
    public interface IGetAllTasksForACUseCase
    {
        List<TaskOutput> Execute();
    }
}
