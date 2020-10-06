using Hoshin.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> CheckUser(User us);
        Task<User> FindByEmail(string email);
        Task<User> Add(User userToCreate, List<string> roles);
        Task<bool> CheckUsernameExists(string username);
        Task<IList<string>> GetRoles(string userId);
        Task<List<User>> GetAll(int id_sector, int id_plant);
        User Get(string id);
        Task<User> Update(User userToUpdate, List<string> roles);
        List<User> GetAll();
        List<User> GetFromJob(int jobId);
        Task<bool> isColaboratorOrSectorBoss(string userId);
        Task<bool> isColaborator(string userId);
        int GetJobFromUser(string userId);
        List<string> GetUsersEmailResponsibleSGC();
        Task ResetPassword(string password, string userId);
    }
}
