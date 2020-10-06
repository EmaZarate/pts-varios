using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.CrossCutting.Authorization.Claims.Core;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class UserWorkflowRepository : IUserWorkflowRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public UserWorkflowRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string GetUserEmailByID(string userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.Users.FirstOrDefault(u => u.Id == userId).Email;
            }
        }

        public List<string> GetUsersEmailResponsibleSGC()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "responsable sgc").Select(x => x.User.Email).ToList();
            }
        }

        public List<string> GetUsersEmailCorrectiveActionApprover()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "aprobador de ac").Select(x => x.User.Email).ToList();
            }
        }

        public List<string> GetUsersEmailSectorBoss()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "jefe de sector").Select(x => x.User.Email).ToList();
            }
        }

        public List<string> GetUsersEmailColaboratorSB()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "colaborador jefe sector").Select(x => x.User.Email).ToList();
            }
        }

        public List<string> GetUsersEmailResponsibleFinding()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "aprobador hallazgos").Select(x => x.User.Email).ToList();
            }
        }

        public string GetFullName(string id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var user = _ctx.Users.FirstOrDefault(x => x.Id == id);
                return user.FirstName + " " +user.Surname;
            }
        }
    }
}
