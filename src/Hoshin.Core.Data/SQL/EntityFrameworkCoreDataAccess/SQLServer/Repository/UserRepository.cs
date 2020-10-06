using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Security.Claims;
using Hoshin.Core.Application.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class UserRepository : Application.Repositories.IUserRepository, Hoshin.CrossCutting.JWT.Repositories.IUserRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        public UserRepository(UserManager<Users> userManager, SQLHoshinCoreContext ctx, IMapper mapper, RoleManager<Roles> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<User> CheckUser(User us)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(us.Username);
                if (user == null) return null;

                if (await _userManager.CheckPasswordAsync(user, us.Password))
                {
                    us.Id = user.Id;
                    us.FirstName = user.FirstName;
                    us.LastName = user.Surname;
                    us.PlantID = user.PlantID;
                    us.SectorID = user.SectorID;
                    us.JobID = user.JobID;
                    us.Active = user.Active;
                    return us;
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<User> Add(User userToCreate, List<string> roles)
        {
            var userMapped = _mapper.Map<User, Users>(userToCreate);
            userMapped.Email = userToCreate.Username;
            userMapped.Status = "Active";

            var identityResult = await _userManager.CreateAsync(userMapped, userToCreate.Password);
            if (identityResult.Succeeded)
            {
                var userCreated = await _userManager.FindByNameAsync(userToCreate.Username);
                await _userManager.AddToRolesAsync(userCreated, roles);

                return _mapper.Map<Users, User>(userCreated);
            }
            return null;
        }

        public async Task<User> Update(User userToUpdate, List<string> roles)
        {
            var actualUser = await _userManager.FindByIdAsync(userToUpdate.Id);
            actualUser.FirstName = userToUpdate.FirstName;
            actualUser.Surname = userToUpdate.LastName;
            actualUser.UserName = userToUpdate.Username;
            actualUser.Email = userToUpdate.Username;
            actualUser.PlantID = userToUpdate.PlantID;
            actualUser.SectorID = userToUpdate.SectorID;
            actualUser.JobID = userToUpdate.JobID;
            actualUser.Address = userToUpdate.Address;
            actualUser.PhoneNumber = userToUpdate.PhoneNumber;
            actualUser.base64Profile = userToUpdate.base64Profile;
            actualUser.Active = userToUpdate.Active;

            var idRes = await _userManager.UpdateAsync(actualUser);
            if (!idRes.Succeeded)
            {
                if (idRes.Errors.FirstOrDefault().Code == "DuplicateUserName")
                {
                    throw new DuplicateEntityException(userToUpdate.Username, "Este nombre de usuario ya existe");
                }

                throw new Exception(idRes.Errors.FirstOrDefault().Description);
            }

            List<string> actualRoles = (await _userManager.GetRolesAsync(actualUser)).ToList();
            var rolesToDelete = actualRoles.Except(roles);
            var rolesToAdd = roles.Except(actualRoles);

            await _userManager.RemoveFromRolesAsync(actualUser, rolesToDelete);
            await _userManager.AddToRolesAsync(actualUser, rolesToAdd);

            return Get(actualUser.Id);
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            var validateIfExists = await _userManager.FindByNameAsync(username);
            if(validateIfExists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<User> FindByEmail(string email)
        {
            var us = await _userManager.FindByEmailAsync(email);
            if (us == null) return null;
            return new User(us.Id, us.UserName, us.FirstName, us.Surname, us.MicrosoftGraphId, us.Email);
        }

        public async Task<List<User>> GetAll(int id_sector, int id_plant)
        {
            
            var users =  _ctx.JobsSectorsPlants.Where(x => x.SectorID == id_sector && x.PlantID == id_plant).Select( x => x.Users).ToList();
            List<Users> usersList = new List<Users>();
            foreach (var list in users)
            {
                List<Users> userList = list.ToList();
                foreach (var user in userList)
                {
                    usersList.Add(user);
                }
            }

            List<Users> listTemp = usersList.OrderBy(x => x.FirstName).ToList();
            return _mapper.Map<List<Users>, List<User>>(listTemp);
        }

        public List<User> GetAll()
        {
            List<User> users = _mapper.Map<List<Users>, List<User>>(
                _ctx.Users
                        .Include(x => x.JobSectorPlant)
                            .ThenInclude(x => x.Job)
                        .Include(x => x.JobSectorPlant)
                            .ThenInclude(x => x.SectorPlant)
                                .ThenInclude(x => x.Sector)
                        .Include(x => x.JobSectorPlant)
                            .ThenInclude(x => x.SectorPlant)
                                .ThenInclude(x => x.Plant)
                        .Include(x => x.UserRoles)
                            .ThenInclude(x => x.Role)
                     .ToList());
            List<User> usersList = new List<User>();


            //List<Users> listTemp = usersList.OrderBy(x => x.FirstName).ToList();
            foreach (var user in users)
            {
                usersList.Add(user);
            }
            List<User> listTemp = usersList.OrderBy(x => x.FirstName).ToList();

            return listTemp;
        }

        public User Get(string id)
        {
            var user = _ctx.Users
                .Include(x => x.JobSectorPlant)
                            .ThenInclude(x => x.SectorPlant)
                                .ThenInclude(x => x.Sector)
                        .Include(x => x.JobSectorPlant)
                            .ThenInclude(x => x.SectorPlant)
                                .ThenInclude(x => x.Plant)
                .FirstOrDefault(x => x.Id == id)
                ;
            return _mapper.Map<Users, User>(user);
        }

        public List<User> GetFromJob(int jobId)
        {
            var user = _ctx.Users
                .Where(x => x.JobID == jobId)
                .ToList();
            return _mapper.Map<List<Users>, List<User>>(user);
        }

        public async Task<List<Claim>> GetClaims(string userName)
        {
            var user = _ctx.Users.FirstOrDefault(x => x.UserName == userName);
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = new List<Claim>();

            foreach (var roleName in userRoles)
            {

                var userRole = await _roleManager.FindByNameAsync(roleName);
                if (userRole.Active)
                {
                    userClaims.AddRange(await _roleManager.GetClaimsAsync(userRole));
                }
            }

            return userClaims;
        }

        public async Task<IList<string>> GetRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> isColaboratorOrSectorBoss(string userId)
        {
            var user = await _ctx.Users
                .Include(x => x.JobSectorPlant)
                    .ThenInclude(x => x.SectorPlant)
                .Where(x => x.Id == userId).FirstOrDefaultAsync();
            if(user.JobID == user.JobSectorPlant.SectorPlant.ReferringJob || user.JobID == user.JobSectorPlant.SectorPlant.ReferringJob2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> isColaborator(string userId)
        {
            var user = await _ctx.Users
                .Include(x => x.JobSectorPlant)
                    .ThenInclude(x => x.SectorPlant)
                .Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user.JobID == user.JobSectorPlant.SectorPlant.ReferringJob2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetJobFromUser(string userId)
        {
            return _ctx.Users.Where(x => x.Id == userId).FirstOrDefault().JobID;
        }

        public List<string> GetUsersEmailResponsibleSGC()
        {
            return _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "responsable sgc").Select(x => x.User.Email).ToList();
        }

        public async Task ResetPassword(string password, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, password);
        }
    }
}
