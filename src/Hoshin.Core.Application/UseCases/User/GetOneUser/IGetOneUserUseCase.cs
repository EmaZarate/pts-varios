using Hoshin.Core.Application.UseCases.User.GetAllUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.User.GetOneUser
{
    public interface IGetOneUserUseCase
    {
        Task<UserOutput> Execute(string id_user);
    }
}
