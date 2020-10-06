using Hoshin.Core.Domain.AlertUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.Repositories
{
    public interface IAlertUserRepository
    {
        Task<List<AlertUser>> GetAllAlertByUser(string userId);
        bool InsertOrUpdate(Dictionary<string, List<Domain.AlertUser.AlertUser>> dicAlertUser);
    }
}
