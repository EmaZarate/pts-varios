using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.LoginUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.User.ResetPasswordUser
{
    public class ResetPasswordUseCase : IResetPasswordUseCase
    {
        private readonly IUserRepository _userRepository;
        public ResetPasswordUseCase(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task Execute(string password, string id)
        {
            if (!String.IsNullOrEmpty(password))
            {
                await _userRepository.ResetPassword(password, id);
            }
        }
    }
}
