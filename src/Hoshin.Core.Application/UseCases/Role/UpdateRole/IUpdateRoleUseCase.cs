using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.UpdateRole
{
    public interface IUpdateRoleUseCase
    {
        Task<bool> Execute(string id, string name, List<string> claims, bool active, bool basic);
    }
}
