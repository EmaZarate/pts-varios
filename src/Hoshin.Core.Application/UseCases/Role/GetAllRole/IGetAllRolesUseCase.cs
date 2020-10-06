using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.GetAllRole
{
    public interface IGetAllRolesUseCase
    {
        Task<List<RoleOutput>> Execute();
    }
}
