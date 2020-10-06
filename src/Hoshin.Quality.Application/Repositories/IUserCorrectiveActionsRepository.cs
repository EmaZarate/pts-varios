using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IUserCorrectiveActionsRepository
    {
        void RemoveAll(int correctiveActionID);
        Task AddRange(int correctiveActionID, List<string> usersID);
        Task SaveChanges();
    }
}
