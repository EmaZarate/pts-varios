using Hoshin.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using UserClaim = Hoshin.CrossCutting.Authorization.Claims.Core.User;
using Hoshin.Core.Application.UseCases.User.GetAllUser;
using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;

namespace Hoshin.Core.Application.UseCases.User.UpdateUser
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UpdateUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserOutput> Execute(ClaimsPrincipal RequestUser, string id, string userName, string password, int jobId, int sectorId, int plantId, string name, string surname, List<string> roles,string address,string phoneNumber, string Base64Profile, bool active)
        {
            var identity = (ClaimsIdentity)RequestUser.Identity;

            var claims = (List<System.Security.Claims.Claim>)identity.Claims;
            string idRequestUser = claims.Find(i => i.Type == "id").Value;

            bool canEdit = false;
            var permissions = claims.FindAll(i => i.Type == "module/permission" && (i.Value == UserClaim.EditUser || i.Value == UserClaim.ActivateUser || i.Value == UserClaim.DeactivateUser));
            if(permissions.Count >= 3)
            {
                canEdit = true;
            }

            if (idRequestUser == id || canEdit)
            {
                var userToUpdate = new Domain.Users.User(id, userName, password, jobId, sectorId, plantId, name, surname, address, phoneNumber, Base64Profile, active);
                var userUpdated = await _userRepository.Update(userToUpdate, roles);
                return _mapper.Map<Domain.Users.User, UserOutput>(userUpdated);
            }
            else
            {
                throw new UnauthorizedException(userName, "No tiene permisos para editar");
            }
        }
    }
}
