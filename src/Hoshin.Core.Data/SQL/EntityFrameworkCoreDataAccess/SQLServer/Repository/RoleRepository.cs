using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Domain.Role;
using Hoshin.CrossCutting.Authorization.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{

    public class RoleRepository : IRoleRepository
    {
        private RoleManager<Roles> _roleManager;
        private readonly IMapper _mapper;
        private readonly SQLHoshinCoreContext _ctx;
        public RoleRepository(RoleManager<Roles> roleManager, IMapper mapper, SQLHoshinCoreContext ctx)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _ctx = ctx;
        }

        public async Task<Role> Add(string role, List<string> claims, bool active, bool basic)
        {
            Roles roleResult;
            var roleCheck = await _roleManager.RoleExistsAsync(role);
            if (roleCheck)
            {
                return null;
            }
            await _roleManager.CreateAsync(new Roles(role) { Active = active, Basic = basic });
            roleResult = await _roleManager.FindByNameAsync(role);

            foreach (var claim in claims)
            {
                await _roleManager.AddClaimAsync(roleResult, new System.Security.Claims.Claim(CustomClaimTypes.Permission, claim));
            }
            return _mapper.Map<Roles, Role>(roleResult);
        }
        public async Task<Role> GetOne(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var mappedRole = _mapper.Map<Roles, Role>(role);
            mappedRole.RoleClaims = _mapper.Map<List<Claim>, List<Domain.Claim.Claim>>((await _roleManager.GetClaimsAsync(role)).ToList());

            return mappedRole;
        }
        public async Task<bool> CheckIfNameExists(string name)
        {
            return await _roleManager.RoleExistsAsync(name);
        }

        public async Task<bool> CheckIfBasicExists()
        {
            var role = _ctx.Roles.Where(x => x.Basic == true && x.Active == true).FirstOrDefault();
            if(role == null)
            {
                return false;
            }
            return true;
        }

        public async Task Update(string id, string role, List<string> claims, bool active, bool basic)
        {
            var roleToUpdate = await this._roleManager.FindByIdAsync(id);

            roleToUpdate.Active = active;
            roleToUpdate.Name = role;
            roleToUpdate.Basic = basic;
            await _roleManager.UpdateAsync(roleToUpdate);

            var actualRoleClaim = await _roleManager.GetClaimsAsync(roleToUpdate);

            await this.UpdateRoleClaims(actualRoleClaim, claims, roleToUpdate);
        } 

        public async Task<List<Role>> GetAll()
        {
            var roles = _ctx.Roles
                                .Include(x => x.RoleClaims)
                                .ToList();

            return _mapper.Map<List<Roles>, List<Role>>(roles);
        }
        public async Task<List<Role>> GetAllRolesActive()
        {
            var roles = _ctx.Roles.Where(x => x.Active == true)
                                .Include(x => x.RoleClaims)
                                .ToList();

            return _mapper.Map<List<Roles>, List<Role>>(roles);
        }

        private async Task UpdateRoleClaims(IList<Claim> actualClaims, List<string> newClaims, Roles roleToUpdate)
        {
            var deletedClaims = actualClaims.Where(x => !newClaims.Contains(x.Value)).ToList();

            foreach (var deletedClaim in deletedClaims)
            {
                await _roleManager.RemoveClaimAsync(roleToUpdate, deletedClaim);
            }
            var existingClaims = actualClaims.Select(x => x.Value).ToList();
            var addedClaims = newClaims.Except(existingClaims);

            foreach (var addedClaim in addedClaims)
            {
                await _roleManager.AddClaimAsync(roleToUpdate, new Claim(CustomClaimTypes.Permission, addedClaim));
            }
        }
    }
}
