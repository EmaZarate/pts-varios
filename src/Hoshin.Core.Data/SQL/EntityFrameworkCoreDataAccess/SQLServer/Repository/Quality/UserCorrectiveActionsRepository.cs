using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class UserCorrectiveActionsRepository : IUserCorrectiveActionsRepository
    {
        private readonly SQLHoshinCoreContext _ctx;

        public UserCorrectiveActionsRepository(SQLHoshinCoreContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddRange(int correctiveActionID, List<string> usersID)
        {
            var listUsersAC = new List<UserCorrectiveAction>();
            foreach (var userID in usersID)
            {
                var userCorrectiveAction = new UserCorrectiveAction()
                {
                    CorrectiveActionID = correctiveActionID,
                    UserID = userID
                };
                listUsersAC.Add(userCorrectiveAction);
            }

            await _ctx.UserCorrectiveActions.AddRangeAsync(listUsersAC);
           
        }

        public void RemoveAll(int correctiveActionID)
        {
            var userCorrectiveActionToDelete = _ctx.UserCorrectiveActions.Where((el) => el.CorrectiveActionID == correctiveActionID).ToList();
            _ctx.UserCorrectiveActions.RemoveRange(userCorrectiveActionToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
