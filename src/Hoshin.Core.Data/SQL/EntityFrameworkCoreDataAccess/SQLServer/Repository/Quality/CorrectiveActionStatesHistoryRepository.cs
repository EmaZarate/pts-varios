using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class CorrectiveActionStatesHistoryRepository : ICorrectiveActionStatesHistoryRepository, Hoshin.Quality.Application.Repositories.ICorrectiveActionStatesHistoryRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public CorrectiveActionStatesHistoryRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public bool Add(int corretiveActionId, int correctiveActionStateId, string createdByUserId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                CorrectiveActionStatesHistory f = new CorrectiveActionStatesHistory()
                {
                    CorrectiveActionID = corretiveActionId,
                    CorrectiveActionStateID = correctiveActionStateId,
                    Date = DateTime.Now,
                    CreatedByUserID = createdByUserId
                };
                _ctx.CorrectiveActionStatesHistory.Add(f);
                _ctx.SaveChanges();

                return true;
            }
        }

        public int GetPreviousState(int correcticeActionID, int actualCorrectiveActionStateId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.CorrectiveActionStatesHistory.Where(x => x.CorrectiveActionID == correcticeActionID && x.CorrectiveActionStateID != actualCorrectiveActionStateId).OrderByDescending(x => x.Date).FirstOrDefault().CorrectiveActionStateID;
            }
        }
    }
}
