using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Tasks.GetAllTask
{
    public interface IGetAllTaskUseCase
    {
        List<TaskOutput> Execute(int id);
        Task<List<TaskOutput>> Execute();
    }
}
