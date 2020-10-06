using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Core.Application.Exceptions.User;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.User.GetAllUser;

namespace Hoshin.Core.Application.UseCases.User.GetOneUser
{
    public class GetOneUserUseCase : IGetOneUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetOneUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserOutput> Execute(string id_user)
        {
            var user = _userRepository.Get(id_user);
            if (user != null)
            {
                user.Roles = await _userRepository.GetRoles(id_user);
                return _mapper.Map<Domain.Users.User, UserOutput>(user);
            }
            throw new UserNotFoundByIdException(id_user, "No se encontro ningún usuario para el id ingresado");
        }
    }
}
