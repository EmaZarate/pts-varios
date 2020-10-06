using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.ParametrizationCorrectiveAction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class ParametrizationCorrectiveActionRepository : CrossCutting.WorkflowCore.Repositories.IParametrizationCorrectiveActionRepository, IParametrizationCorrectiveActionRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public ParametrizationCorrectiveActionRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ParametrizationCorrectiveAction Add(ParametrizationCorrectiveAction newparam)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var param = _ctx.ParametrizationCorrectiveActions.Where(x => x.Name == newparam.Name).FirstOrDefault();
                if (param == null)
                {
                    var paramCorrectiveAction = new ParametrizationCorrectiveActions();
                    paramCorrectiveAction.Name = newparam.Name;
                    paramCorrectiveAction.Code = newparam.Code;
                    paramCorrectiveAction.Value = newparam.Value;


                    _ctx.ParametrizationCorrectiveActions.Add(paramCorrectiveAction);
                    _ctx.SaveChanges();

                    newparam.Id = paramCorrectiveAction.ParametrizationCorrectiveActionID;
                    return newparam;
                }
                return null;
            }
        }

        public ParametrizationCorrectiveAction Get(string name, string code)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var param = _ctx.ParametrizationCorrectiveActions.Where(x => x.Name == name || x.Code == code).FirstOrDefault();
                if (param != null)
                {
                    return new ParametrizationCorrectiveAction(param.Name, param.Code, param.Value, param.ParametrizationCorrectiveActionID);
                }
                return null;
            }
        }

        public ParametrizationCorrectiveAction Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var param = _ctx.ParametrizationCorrectiveActions.Where(x => x.ParametrizationCorrectiveActionID == id).FirstOrDefault();
                if (param != null)
                {
                    return new ParametrizationCorrectiveAction(param.Name, param.Code, param.Value, param.ParametrizationCorrectiveActionID);
                }
                return null;
            }
        }

        public int GetByCode(string code)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                return _ctx.ParametrizationCorrectiveActions.Where(x => x.Code == code).FirstOrDefault().Value;
            }
        }

        public List<ParametrizationCorrectiveAction> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var res = _ctx.ParametrizationCorrectiveActions.ToList();
                var list = new List<ParametrizationCorrectiveAction>();
                foreach (var pc in res)
                {
                    list.Add(new ParametrizationCorrectiveAction(pc.Name, pc.Code, pc.Value, pc.ParametrizationCorrectiveActionID));
                }
                return list;
            }
        }

        public ParametrizationCorrectiveAction Update(ParametrizationCorrectiveAction updateparam)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var paramCorrectiveAction = _ctx.ParametrizationCorrectiveActions.Where(x => x.ParametrizationCorrectiveActionID == updateparam.Id).FirstOrDefault();
                paramCorrectiveAction.Name = updateparam.Name;
                paramCorrectiveAction.Code = updateparam.Code;
                paramCorrectiveAction.Value = updateparam.Value;
                _ctx.Update(paramCorrectiveAction);
                _ctx.SaveChanges();

                return updateparam;
            }
        }

        public ParametrizationCorrectiveAction GetByName(string name)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var param = _ctx.ParametrizationCorrectiveActions.Where(x => x.Name == name).FirstOrDefault();
                if (param != null)
                {
                    return new ParametrizationCorrectiveAction(param.Name, param.Code, param.Value, param.ParametrizationCorrectiveActionID);
                }
                return null;
            }
        }
    }
}