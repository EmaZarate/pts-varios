using Hoshin.Core.Application.UseCases.User.GetAllUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.User.AddUser
{
    public interface IAddUserUseCase
    {
        Task<UserOutput> Execute(string userName, string password, int jobId, int sectorId, int plantId, string name, string surname, List<string> roles, bool active);
    }
}
