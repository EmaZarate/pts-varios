using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Domain.TaskState;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class TaskStateRepository : Hoshin.Quality.Application.Repositories.ITaskStateRepository, CrossCutting.WorkflowCore.Repositories.ITaskStateRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public TaskStateRepository(IServiceProvider ServiceProvider)
        {
            _serviceProvider = ServiceProvider;
        }

      

        public TaskState Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var taskState = _ctx.TaskStates.Where(x => x.TaskStateID == id).FirstOrDefault();
                //return _mapper.Map<TaskStates, TaskState>(taskState);
                if (taskState != null) {
                    return new TaskState(taskState.Code, taskState.Name, taskState.Color, taskState.Active, taskState.TaskStateID);
                }
                return null;
            }
        }

        public TaskState GetByName(string name)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var taskState = _ctx.TaskStates.Where(x => x.Name == name && x.Active).FirstOrDefault();
                if (taskState != null)
                {
                    return new TaskState(taskState.Code, taskState.Name, taskState.Color, taskState.Active, taskState.TaskStateID);
                }
                return null;
            }
        }

        public List<TaskState> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {

                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var res = _ctx.TaskStates.ToList();
                var list = new List<TaskState>();
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                // return _mapper.Map<List<TaskStates>, List<TaskState>>(res);
                foreach (TaskStates ts in res)
                {
                    list.Add(new TaskState(ts.Code, ts.Name, ts.Color, ts.Active, ts.TaskStateID));
                }
                return list;
            }

        }

        public TaskState Update(TaskState updateTaskState)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var taskState = _ctx.TaskStates.Where(x => x.TaskStateID == updateTaskState.TaskStateID).FirstOrDefault();
                taskState.Name = updateTaskState.Name;
                taskState.Color = updateTaskState.Color;
                taskState.Code = updateTaskState.Code;
                taskState.Active = updateTaskState.Active;
                _ctx.Update(taskState);
                _ctx.SaveChanges();
                return updateTaskState;
            }

        }

        public TaskState Add(TaskState newParam)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var taskState = new TaskStates();
                taskState.Active = newParam.Active;
                taskState.Code = newParam.Code;
                taskState.Color = newParam.Color;
                taskState.Name = newParam.Name;
                _ctx.TaskStates.Add(taskState);
                _ctx.SaveChanges();
                newParam.TaskStateID = taskState.TaskStateID;
                return newParam;
            }
        }

        public TaskState CheckExistsTaskState(string code, string name, string colour, int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var param = _ctx.TaskStates.Where(x => (x.Code == code || x.Name == name || x.Color == colour) && x.TaskStateID != id).FirstOrDefault();
                if (param != null)
                {
                    return new TaskState(param.Code, param.Name, param.Color, param.Active, param.TaskStateID);
                }
                return null;
            }
        }

        public TaskState Get(string code, string name, string color)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var param = _ctx.TaskStates.Where(x => x.Code == code || x.Color == color || x.Name == name).FirstOrDefault();
                if (param != null)
                {
                    return new TaskState(param.Code, param.Name, param.Color, param.Active, param.TaskStateID);
                }
                return null;
            }
        }

        public int GetIdByCode(string code)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.TaskStates.FirstOrDefault(t => t.Code == code).TaskStateID;
            }
        }
    }
}
        