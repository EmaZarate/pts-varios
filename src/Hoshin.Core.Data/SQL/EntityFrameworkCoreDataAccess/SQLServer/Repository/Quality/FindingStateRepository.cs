using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.FindingsState;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class FindingStateRepository : IFindingStateRepository, CrossCutting.WorkflowCore.Repositories.IFindingStateRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public FindingStateRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public List<FindingsState> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var res = _ctx.FindingsStates.ToList();
                var list = new List<FindingsState>();
                foreach (FindingsStates fs in res)
                {
                    list.Add(new FindingsState(fs.Code, fs.Name, fs.Colour, fs.Active, fs.FindingStateID));
                }
                return list;
            }


        }

        public FindingsState Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var param = _ctx.FindingsStates.Where(x => x.FindingStateID == id).FirstOrDefault();
                if (param != null)
                {
                    return new FindingsState(param.Code, param.Name, param.Colour, param.Active, param.FindingStateID);
                }

                return null; 
            }
        }

        public FindingsState Get(string code, string name, string colour)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var param = _ctx.FindingsStates.Where(x => x.Code == code || x.Name == name || x.Colour == colour).FirstOrDefault();
                if (param != null)
                {
                    return new FindingsState(param.Code, param.Name, param.Colour, param.Active, param.FindingStateID);
                }

                return null; 
            }
        }

        public FindingsState CheckExistsFindingState(string code, string name, string colour, int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var param = _ctx.FindingsStates.Where(x => (x.Code == code || x.Name == name || x.Colour == colour) && x.FindingStateID != id).FirstOrDefault();
                if (param != null)
                {
                    return new FindingsState(param.Code, param.Name, param.Colour, param.Active, param.FindingStateID);
                }

                return null;
            }
        }

        public FindingsState Add(FindingsState newparam)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var findingsState = new FindingsStates();
                findingsState.Code = newparam.Code;
                findingsState.Name = newparam.Name;
                findingsState.Colour = newparam.Colour;
                findingsState.Active = newparam.Active;

                _ctx.FindingsStates.Add(findingsState);
                _ctx.SaveChanges();

                newparam.Id = findingsState.FindingStateID;

                return newparam; 
            }
        }

        public FindingsState Update(FindingsState updateparam)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var findingsState = _ctx.FindingsStates.Where(x => x.FindingStateID == updateparam.Id).FirstOrDefault();
                findingsState.Name = updateparam.Name;
                findingsState.Colour = updateparam.Colour;
                findingsState.Code = updateparam.Code;
                findingsState.Active = updateparam.Active;
                _ctx.Update(findingsState);
                _ctx.SaveChanges();

                return updateparam; 
            }
        }

        public int GetOneByCode(string code)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.FindingsStates.Where(x => x.Code == code).FirstOrDefault().FindingStateID;
            }
        }
    }
}
