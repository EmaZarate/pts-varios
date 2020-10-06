using Hoshin.Quality.Domain.TaskState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
   public interface ITaskStateRepository
    {
        List<TaskState> GetAll();
        TaskState Get(int id);
        TaskState GetByName(string name);
        TaskState Get(string code, string name, string color);
        TaskState Add(TaskState newTaskState);
        TaskState Update(TaskState updateTaskState);
        TaskState CheckExistsTaskState(string code, string name, string colour, int id);
        int GetIdByCode(string code);
    }
}
