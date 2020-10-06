using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Role;

namespace Hoshin.Core.Application.UseCases.Role.AddRole
{
    public class AddRoleUseCase : IAddRoleUseCase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public AddRoleUseCase(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<RoleOutput> Execute(string name, List<string> claims, bool active, bool basic)
        {
            var role = await _roleRepository.Add(name, claims, active, basic);
            if(role == null)
            {
                throw new DuplicateEntityException(name, "Ya existe un rol con ese nombre");
            }

            return _mapper.Map<Domain.Role.Role, RoleOutput>(role);
        }
    }
}
