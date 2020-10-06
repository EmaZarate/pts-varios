using Hoshin.Core.Domain.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> Add(string role, List<string> claims, bool active, bool basic);
        Task<bool> CheckIfNameExists(string name);
        Task<bool> CheckIfBasicExists();
        Task<Role> GetOne(string id);
        Task Update(string id,string role, List<string> claims, bool active, bool basic);
        Task<List<Role>> GetAll();
        Task<List<Role>> GetAllRolesActive();
    }
}
