using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.LoginUser
{
    public interface ILoginUserUseCase
    {
        Task<UserOutput> Execute(string user, string password, bool isLocked = false);
        Task<UserOutput> Execute(string accessToken);
    }
}
