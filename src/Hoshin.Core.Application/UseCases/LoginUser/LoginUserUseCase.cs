using Hoshin.Core.Application.Exceptions.User;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Users;
using Hoshin.CrossCutting.JWT;
using Hoshin.CrossCutting.JWT.Factory;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces;
using Hoshin.CrossCutting.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Hoshin.Core.Application.UseCases.LoginUser
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IStorage _storage;
        public LoginUserUseCase(IUserRepository userRepository, IJwtService jwtService, IUserService userService, IStorage storage)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _userService = userService;
            _storage = storage;
        }
        public async Task<UserOutput> Execute(string user, string password,bool isLocked = false)
        {
            var us = await _userRepository.CheckUser(new Hoshin.Core.Domain.Users.User(user, password));
            if(us == null)
            {
                if (isLocked) throw new UserNotFoundException(user, password, "Contraseña incorrecta", 436);
                throw new UserNotFoundException(user, password, "Usuario o contraseña incorrecto", 436);
            }
            if (!us.Active)
            {
                throw new UserNotFoundException(user, password, "Usuario inactivo", 436);
            }
            var jwt = await _jwtService.GenerateJWT(user, us.Id, us.PlantID, us.SectorID, us.JobID);

            return new UserOutput(jwt);
        }

        public async Task<UserOutput> Execute(string accessToken)
        {
            var token = await _userService.GetAccessToken(accessToken);
            var userMsft = await _userService.GetMe(token.AccessToken);
            _storage.StoreToken(userMsft.Id, token);

            var usHoshin = await _userRepository.FindByEmail(userMsft.Mail);
            if(usHoshin == null)
            {
                //Create new account
            }
            var jwt = await _jwtService.GenerateJWT(usHoshin.Username, usHoshin.Id, usHoshin.PlantID, usHoshin.SectorID, usHoshin.JobID);

            return new UserOutput(jwt);
        }
    }
}
