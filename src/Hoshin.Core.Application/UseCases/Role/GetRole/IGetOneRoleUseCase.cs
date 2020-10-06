using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.GetRole
{
    public interface IGetOneRoleUseCase
    {
        Task<RoleOutput> Execute(string idRole);
    }
}
