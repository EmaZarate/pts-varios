using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveActionState;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using System.Linq;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class CorrectiveActionStateRepository : ICorrectiveActionStateRepository, Hoshin.CrossCutting.WorkflowCore.Repositories.ICorrectiveActionStateRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public CorrectiveActionStateRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public List<CorrectiveActionState> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActionStates = _ctx.CorrectiveActionStates.ToList();
                return _mapper.Map<List<CorrectiveActionStates>, List<CorrectiveActionState>>(correctiveActionStates);
            }
        }

        public CorrectiveActionState Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActionState = _ctx.CorrectiveActionStates.Where(x => x.CorrectiveActionStateID == id).SingleOrDefault();
                return _mapper.Map<CorrectiveActionStates, CorrectiveActionState>(correctiveActionState);
            }
        }

        public CorrectiveActionState Get(string code, string name, string color)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActionState = _ctx.CorrectiveActionStates.Where(x => x.Code == code || x.Name == name || x.Color == color).SingleOrDefault();
                if(correctiveActionState != null)
                {
                    return _mapper.Map<CorrectiveActionStates, CorrectiveActionState>(correctiveActionState);
                }

                return null;
            }
        }

        public CorrectiveActionState Add(CorrectiveActionState newCorrectiveActionState)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActionState = _mapper.Map<CorrectiveActionState, CorrectiveActionStates>(newCorrectiveActionState);

                _ctx.CorrectiveActionStates.Add(correctiveActionState);
                _ctx.SaveChanges();

                return _mapper.Map<CorrectiveActionStates, CorrectiveActionState>(correctiveActionState);
            }
        }

        public CorrectiveActionState Update(CorrectiveActionState updateCorrectiveActionState)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActionState = _mapper.Map<CorrectiveActionState, CorrectiveActionStates>(updateCorrectiveActionState);

                _ctx.CorrectiveActionStates.Update(correctiveActionState);
                _ctx.SaveChanges();

                return _mapper.Map<CorrectiveActionStates, CorrectiveActionState>(correctiveActionState);
            }
        }

        public CorrectiveActionState CheckDuplicates(CorrectiveActionState checkCorrectiveActionState)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActionState = _ctx.CorrectiveActionStates.Where(x => (x.Code == checkCorrectiveActionState.Code || x.Name == checkCorrectiveActionState.Name || x.Color == checkCorrectiveActionState.Color) && x.CorrectiveActionStateID != checkCorrectiveActionState.CorrectiveActionStateID).FirstOrDefault();
                if(correctiveActionState != null)
                {
                    return _mapper.Map<CorrectiveActionStates, CorrectiveActionState>(correctiveActionState);
                }

                return null;
            }
        }

        public int GetByCode(string code)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
               

                return _ctx.CorrectiveActionStates.FirstOrDefault(c => c.Code == code).CorrectiveActionStateID;
            }
        }
    }
}
