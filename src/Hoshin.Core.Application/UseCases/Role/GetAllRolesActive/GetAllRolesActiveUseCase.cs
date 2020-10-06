using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Core.Application.Repositories;

namespace Hoshin.Core.Application.UseCases.Role.GetAllRolesActive
{
    public class GetAllRolesActiveUseCase:IGetAllRolesActiveUseCase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public GetAllRolesActiveUseCase(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleOutput>> Execute()
        {
            return _mapper.Map<List<Domain.Role.Role>, List<RoleOutput>>(await _roleRepository.GetAllRolesActive());
        }
    }
}
