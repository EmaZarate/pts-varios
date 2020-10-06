using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class TaskRepository : ITaskRepository, Hoshin.CrossCutting.WorkflowCore.Repositories.ITaskRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private const int TASK_CORRECTIVE_ACTION = 1;
        private const string TASK_STATE_BORRADOR = "borrador";

        public TaskRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;  
        }

        public Task Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var task = _ctx.Tasks.Where(x => x.TaskID == id).Include(x => x.ResponsibleUser).Include(x => x.TaskEvidences).Include(x => x.TaskState).FirstOrDefault();
                return _mapper.Map<Tasks, Task>(task);
            }
        }

        public List<Task> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                    var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                    var entities = _ctx.Tasks.Include(x => x.ResponsibleUser).Include(x => x.TaskState)
                        .Where(x => x.TaskState.Code != TASK_STATE_BORRADOR).OrderByDescending(x => x.TaskID).ToList();
                    return _mapper.Map<List<Tasks>, List<Task>>(entities);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Task> GetAllFromUser(string idUser)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                    var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                    var entities = _ctx.Tasks.Include(x => x.ResponsibleUser).Include(x => x.TaskState)
                        .Where(x => x.TaskState.Code != TASK_STATE_BORRADOR && x.ResponsibleUserID == idUser)
                        .OrderByDescending(x => x.TaskID).ToList();
                    return _mapper.Map<List<Tasks>, List<Task>>(entities);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Task> GetAllFromUserAndReferring(string idUser)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                    var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                    var _userRepository = scope.ServiceProvider.GetService(typeof(IUserRepository)) as IUserRepository;
                    List<Tasks> entitiesReturn = new List<Tasks>();
                    var colaboratorUser = _userRepository.Get(idUser);
                    var idjob = colaboratorUser.JobSectorPlant.SectorPlant.ReferringJob;

                    var boosUsers = _userRepository.GetFromJob(idjob);

                    var entities = _ctx.Tasks.Include(x => x.ResponsibleUser).Include(x => x.TaskState)
                        .Where(x => x.TaskState.Code != TASK_STATE_BORRADOR)
                        .OrderByDescending(x => x.TaskID).ToList();

                    foreach (var user in boosUsers)
                    {
                        entitiesReturn.AddRange(entities.Where(x => x.ResponsibleUserID == user.Id));
                    }
                    entitiesReturn.AddRange(entities.Where(x => x.ResponsibleUserID == idUser));

                    return _mapper.Map<List<Tasks>, List<Task>>(entitiesReturn);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Task> GetAllForCorrectiveAction(int correctiveActionId)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                var correctiveActionTasks = _ctx.Tasks.Where(t => t.EntityID == correctiveActionId && t.EntityType == TASK_CORRECTIVE_ACTION)
                    .Include(x => x.ResponsibleUser)
                    .Include(x => x.TaskState)
                    .Where(x => x.TaskState.Code == TASK_STATE_BORRADOR).ToList();

                return _mapper.Map<List<Task>>(correctiveActionTasks);
            }
        }

        public List<TaskWorkflowData> GetAllForCorrectiveActionWorkflow(int correctiveActionId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                var correctiveActionTasks = _ctx.Tasks.Where(t => t.EntityID == correctiveActionId && t.EntityType == TASK_CORRECTIVE_ACTION)
                    .Include(x => x.ResponsibleUser)
                    .ToList();

                return _mapper.Map<List<TaskWorkflowData>>(correctiveActionTasks);
            }
        }

        public List<Task> GetAllForCorrectiveActionWithOutStates(int correctiveActionId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                var correctiveActionTasks = _ctx.Tasks.Where(t => t.EntityID == correctiveActionId && t.EntityType == TASK_CORRECTIVE_ACTION)
                    .Include(x => x.ResponsibleUser)
                    .Include(x => x.TaskState)
                    .ToList();

                return _mapper.Map<List<Task>>(correctiveActionTasks);
            }
        }

        public List<string> GetAllResponsibleUserEmailForCorrectiveAction(int correctiveActionId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                var emails = _ctx.Tasks.Where(t => t.EntityID == correctiveActionId && t.EntityType == TASK_CORRECTIVE_ACTION)
                    .Include(x => x.ResponsibleUser)
                    .Select(x => x.ResponsibleUser.Email).ToList();
                return emails;
            }
        }

        public Task Add(Task newTask)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var task = new Tasks()
                {
                    Description = newTask.Description,
                    ResponsibleUserID = newTask.ResponsibleUserID,
                    ImplementationPlannedDate = newTask.ImplementationPlannedDate,
                    RequireEvidence = newTask.RequireEvidence,
                    ImplementationEffectiveDate = null,
                    Observation = null,
                    Result = null,
                    TaskStateID = newTask.TaskStateID,
                    EntityID = newTask.EntityID,
                    EntityType = TASK_CORRECTIVE_ACTION
                };

                _ctx.Tasks.Add(task);
                _ctx.SaveChanges();
                newTask.TaskID = task.TaskID;
                return newTask;
            }
        }

        public Task Update(Task editTask)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var task = _ctx.Tasks.Where(x => x.TaskID == editTask.TaskID).FirstOrDefault();
                task.Description = editTask.Description;
                task.ResponsibleUserID = editTask.ResponsibleUserID;
                task.ImplementationPlannedDate = editTask.ImplementationPlannedDate;
                task.RequireEvidence = editTask.RequireEvidence;
                task.TaskStateID = editTask.TaskStateID;
                _ctx.Update(task);
                _ctx.SaveChanges();
                return editTask;
            }
        }

        public Task UpdateTask(Task updateTask)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var task = _ctx.Tasks.Where(x => x.TaskID == updateTask.TaskID).FirstOrDefault();
                task.Observation = updateTask.Observation;
                task.TaskStateID = updateTask.TaskStateID;
                _ctx.Update(task);
                _ctx.SaveChanges();
                return updateTask;
            }
        }

        public void Delete(Task deleteTask)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var task = _ctx.Tasks.Where(x => x.TaskID == deleteTask.TaskID).FirstOrDefault();
                _ctx.Remove(task);
                _ctx.SaveChanges();
            }
        }

        public int GetCount()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                return _ctx.Tasks.Include(x => x.TaskState).Where(x => x.TaskState.Code != TASK_STATE_BORRADOR ).Count();
            }
        }
    }
}
