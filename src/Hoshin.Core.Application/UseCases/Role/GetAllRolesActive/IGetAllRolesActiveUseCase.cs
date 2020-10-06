using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.GetAllRolesActive
{
    public interface IGetAllRolesActiveUseCase
    {
        Task<List<RoleOutput>> Execute();
    }
}
