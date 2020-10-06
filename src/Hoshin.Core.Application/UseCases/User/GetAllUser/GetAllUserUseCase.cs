using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Core.Application.Exceptions.User;
using Hoshin.Core.Application.Repositories;

namespace Hoshin.Core.Application.UseCases.User.GetAllUser
{
    public class GetAllUserUseCase : IGetAllUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserOutput>> Execute(int id_sector, int id_plant)
        {
            var users = await _userRepository.GetAll(id_sector, id_plant);
            if (users.Count != 0)
            {
                return _mapper.Map<List<Domain.Users.User>, List<UserOutput>>(users);
            }
            else
            {
                throw new UsersNotFoundException(id_sector,id_plant, "No se encontraron usuarios para el sector y planta ingresados");
            }
            
        }

        public List<UserOutput> Execute()
        {
            return _mapper.Map<List<Domain.Users.User>, List<UserOutput>>(_userRepository.GetAll()); 
        }
    }
}
