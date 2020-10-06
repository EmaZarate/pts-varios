using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using AspectStates = Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality.AspectStates;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class AspectStatesRepository : Hoshin.Quality.Application.Repositories.IAspectStatesRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public AspectStatesRepository(IServiceProvider serviceProvider, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }
        public List<Hoshin.Quality.Domain.AspectStates.AspectStates> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var res = _ctx.AspectStates.ToList();
                return _mapper.Map<List<AspectStates>, List<Hoshin.Quality.Domain.AspectStates.AspectStates>>(res);
            }
        }

        public Hoshin.Quality.Domain.AspectStates.AspectStates GetOne(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var aspectStateRepository = _ctx.AspectStates.Where(x => x.AspectStateID == id).FirstOrDefault();
                if (aspectStateRepository != null)
                {
                    return _mapper.Map<AspectStates, Hoshin.Quality.Domain.AspectStates.AspectStates>(aspectStateRepository);
                    //return new Hoshin.Quality.Domain.AspectStates.AspectStates(aspectStateRepository.AspectStateID, aspectStateRepository.Name, aspectStateRepository.Color, aspectStateRepository.Active);
                }

                return null;
            }
        }

        public Hoshin.Quality.Domain.AspectStates.AspectStates Get(string name, string colour)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var aspectState = _ctx.AspectStates.Where(x => x.Name == name || x.Colour == colour).FirstOrDefault();
                if (aspectState != null)
                {
                    return _mapper.Map<AspectStates, Hoshin.Quality.Domain.AspectStates.AspectStates>(aspectState);
                }

                return null;
            }
        }

        public Hoshin.Quality.Domain.AspectStates.AspectStates CheckExistsAspectState(string name, string colour, int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var aspectState = _ctx.AspectStates.Where(x => (x.Name == name || x.Colour == colour) && (x.AspectStateID != id)).FirstOrDefault();
                if (aspectState != null)
                {
                    return _mapper.Map<AspectStates, Hoshin.Quality.Domain.AspectStates.AspectStates>(aspectState);
                }

                return null;
            }
        }



        public Hoshin.Quality.Domain.AspectStates.AspectStates Update(Hoshin.Quality.Domain.AspectStates.AspectStates aspectStatus)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var aspectStatesResult = _ctx.AspectStates.Where(x => x.AspectStateID == aspectStatus.AspectStateID).FirstOrDefault();
                aspectStatesResult.Name = aspectStatus.Name;
                aspectStatesResult.Colour = aspectStatus.Colour;
                aspectStatesResult.Active = aspectStatus.Active;
                _ctx.Update(aspectStatesResult);
                _ctx.SaveChanges();

                return aspectStatus;
            }
        }

        public Hoshin.Quality.Domain.AspectStates.AspectStates Add(Hoshin.Quality.Domain.AspectStates.AspectStates aspectStatus)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var aspectState = new AspectStates();
                aspectState.Name = aspectStatus.Name;
                aspectState.Colour = aspectStatus.Colour;
                aspectState.Active = aspectStatus.Active;

                _ctx.AspectStates.Add(aspectState);
                _ctx.SaveChanges();

                aspectStatus.AspectStateID = aspectState.AspectStateID;

                return aspectStatus;
            }
        }

        public Int32 GetAspectStateID(string name)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.AspectStates.Where(x => x.Name == name).FirstOrDefault().AspectStateID;
            }
        }
    }
}
