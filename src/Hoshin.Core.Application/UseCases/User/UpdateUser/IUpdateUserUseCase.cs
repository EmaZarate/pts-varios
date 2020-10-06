using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Hoshin.Core.Application.UseCases.User.GetAllUser;

namespace Hoshin.Core.Application.UseCases.User.UpdateUser
{
    public interface IUpdateUserUseCase
    {
        Task<UserOutput> Execute(ClaimsPrincipal RequestUser, string id, string userName, string password, int jobId, int sectorId, int plantId, string name, string surname, List<string> roles,string Address, string phoneNumber, string img, bool active);
    }
}
