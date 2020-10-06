using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.User.GetAllUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.User.AddUser
{
    public class AddUserUseCase : IAddUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AddUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserOutput> Execute(string userName, string password, int jobId, int sectorId, int plantId, string name, string surname, List<string> roles, bool active)
        {
            if (await _userRepository.CheckUsernameExists(userName))
            {
                throw new DuplicateEntityException(userName, "Ya existe un usuario con ese nombre");
            }
            var userToCreate = new Domain.Users.User(userName, password, jobId, sectorId, plantId, name, surname, active);
            var userCreated = await _userRepository.Add(userToCreate, roles);

            return _mapper.Map<Domain.Users.User, UserOutput>(userCreated);
        }
    }
}
