using Hoshin.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.UpdateRole
{
    public class UpdateRoleUseCase : IUpdateRoleUseCase
    {
        private readonly IRoleRepository _roleRepository;
        public UpdateRoleUseCase(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<bool> Execute(string id, string role, List<string> claims, bool active,bool basic)
        {
            await _roleRepository.Update(id, role, claims, active, basic);
            return true;
        }
    }
}
