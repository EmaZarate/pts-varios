using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.JWT.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get all claims for each role
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<List<Claim>> GetClaims(string userName);
    }
}
