using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.GetRole
{
    public class GetOneRoleUseCase : IGetOneRoleUseCase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public GetOneRoleUseCase(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleOutput> Execute(string idRole)
        {
            var role = await _roleRepository.GetOne(idRole);
            if(role == null)
            {
                throw new EntityNotFoundException(idRole, "No existe un rol con ese ID");
            }
            return _mapper.Map<Domain.Role.Role, RoleOutput>(role);
        }
    }
}
