using Hoshin.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.CheckIfNameExists
{
    public class CheckIfNameExistsUseCase : ICheckIfNameExistsUseCase
    {
        private readonly IRoleRepository _roleRepository;
        public CheckIfNameExistsUseCase(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<bool> Execute(string name)
        {
            return await _roleRepository.CheckIfNameExists(name);
        }
    }
}
