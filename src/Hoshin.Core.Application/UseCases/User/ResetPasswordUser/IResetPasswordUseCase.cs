using Hoshin.Core.Application.UseCases.LoginUser;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.User.ResetPasswordUser
{
    public interface IResetPasswordUseCase
    {
        Task Execute(string password, string id);
    }
}
