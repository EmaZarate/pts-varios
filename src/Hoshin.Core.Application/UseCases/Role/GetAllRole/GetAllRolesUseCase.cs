using AutoMapper;
using Hoshin.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.GetAllRole
{
    public class GetAllRolesUseCase : IGetAllRolesUseCase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetAllRolesUseCase(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<List<RoleOutput>> Execute()
        {
            return _mapper.Map<List<Domain.Role.Role>, List<RoleOutput>>(await _roleRepository.GetAll());   
        }
    }
}
