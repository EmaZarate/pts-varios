using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hoshin.Core.Application.Repositories;

namespace Hoshin.Core.Application.UseCases.Role.CheckIfBasicExists
{
    public class CheckIfBasicExistsUseCase: ICheckIfBasicExistsUseCase
    {
        private readonly IRoleRepository _roleRepository;
        public CheckIfBasicExistsUseCase(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Execute()
        {
            return await _roleRepository.CheckIfBasicExists();
        }
    }
}
