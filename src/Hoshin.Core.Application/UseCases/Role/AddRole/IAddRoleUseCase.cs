using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.AddRole
{
    public interface IAddRoleUseCase
    {
        Task<RoleOutput> Execute(string name, List<string> claims, bool active, bool basic);
    }
}
