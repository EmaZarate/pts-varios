using Hoshin.CrossCutting.MicrosoftGraph.DTO.User;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get logged user.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<MicrosoftGraphUserData> GetMe(string accessToken);
        Task<MicrosoftGraphAppAccessToken> RefreshAccessToken(string accessToken);
        Task<MicrosoftGraphAppAccessToken> GetAccessToken(string accessToken);
    }
}
