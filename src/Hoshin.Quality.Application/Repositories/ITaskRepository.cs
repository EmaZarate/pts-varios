using Hoshin.Quality.Domain.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface ITaskRepository
    {
       
        Task Update(Task updateTask);     
        Task Get(int id);
        List<Task> GetAll();
        List<Task> GetAllFromUser(string userId);
        Task Add(Task newTask);
        List<Task> GetAllForCorrectiveAction(int correctiveActionId);
        List<Task> GetAllForCorrectiveActionWithOutStates(int correctiveActionId);
        List<Task> GetAllFromUserAndReferring(string idUser);
        void Delete(Task deleteTask);
        Task UpdateTask(Task updateTask);
        int GetCount();
    }
}
