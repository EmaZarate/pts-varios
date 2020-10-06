using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Hoshin.Quality.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class FindingStatesHistoryRepository : IFindingStatesHistoryRepository, IFindingStateHistoryRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public FindingStatesHistoryRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool Add(int findingId, int findingStateId, string createdByUserId)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                FindingsStatesHistory f = new FindingsStatesHistory()
                {
                    FindingID = findingId,
                    FindingStateID = findingStateId,
                    Date = DateTime.Now,
                    CreatedByUserID = createdByUserId
                };
                _ctx.FindingsStatesHistories.Add(f);
                _ctx.SaveChanges();

                return true;
            }
        }

        public int GetPreviousState(int findingID, int actualFindingStateId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                int state = _ctx.FindingsStatesHistories.Where(x => x.FindingID == findingID && x.FindingStateID != actualFindingStateId).OrderByDescending(x => x.Date).FirstOrDefault().FindingStateID;
                return state;    
            }
        }
    }
}
