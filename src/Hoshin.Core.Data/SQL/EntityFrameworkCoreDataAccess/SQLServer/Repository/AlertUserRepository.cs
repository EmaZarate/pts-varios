using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Domain.AlertUser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class AlertUserRepository : IAlertUserRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;

        public AlertUserRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<List<AlertUser>> GetAllAlertByUser(string userId)
        {
            var list = await _ctx.AlertUsers.Include(x => x.Alert).Include(x=>x.Users).Where(x => x.UsersID == userId).ToListAsync();
            List<AlertUser> s = _mapper.Map<List<Entities.AlertUsers>, List<AlertUser>>(list);
            return s;
        }

        public bool InsertOrUpdate(Dictionary<string, List<AlertUser>> dicAlertUser)
        {
            foreach (string alertType in dicAlertUser.Keys)
            {
                List<Domain.AlertUser.AlertUser> listAlertUser = dicAlertUser[alertType];
                foreach (Domain.AlertUser.AlertUser alertUser in listAlertUser)
                {
                    Entities.AlertUsers alertUserNewOrUpdate = null;

                    if (alertUser.AlertUsersID <= 0)
                    {
                       
                        alertUserNewOrUpdate = new Entities.AlertUsers();
                        alertUserNewOrUpdate.AlertID = alertUser.AlertID;
                        alertUserNewOrUpdate.GenerateAlert = alertUser.GenerateAlert;
                        alertUserNewOrUpdate.UsersID = alertUser.UsersID;
                        _ctx.AlertUsers.Add(alertUserNewOrUpdate);
                    }
                    else
                    {
                        alertUserNewOrUpdate = _ctx.AlertUsers.Where(x => x.AlertUsersID == alertUser.AlertUsersID).SingleOrDefault();                        
                        alertUserNewOrUpdate.GenerateAlert = alertUser.GenerateAlert;                        
                        _ctx.AlertUsers.Update(alertUserNewOrUpdate);
                    }
                }
            }

            _ctx.SaveChanges();
            return true;
        }
    }
}
