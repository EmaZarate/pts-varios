using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Quality.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class AspectRepository : IAspectRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public AspectRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Hoshin.Quality.Domain.Aspect.Aspect GetOne(int standardId, int aspectId)
        {
            using(var _scope = _serviceProvider.CreateScope())
            {
                var _ctx = _scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _mapper = _scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                return _mapper.Map<Entities.Quality.Aspects, Hoshin.Quality.Domain.Aspect.Aspect>(
                    _ctx.Aspects.Where(x => x.AspectID == aspectId && x.StandardID == standardId)
                        .FirstOrDefault());
            }
        }
    }
}
