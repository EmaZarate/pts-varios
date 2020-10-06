using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskModel = Hoshin.Quality.Domain.Task;

namespace Hoshin.Quality.Application.UseCases.Tasks.UpdateTask
{
    public interface IUpdateTaskUseCase
    {
        TaskOutput Execute(TaskModel.Task task);
        Task Execute(TaskModel.Task task, IFormFile[] evidences, List<string> filesToDelete);
        Task Execute(TaskModel.Task task, string observation, DateTime overdureTime,int TaskID);
    
    }
}
