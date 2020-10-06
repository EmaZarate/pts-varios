using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class SectorPlantRepository : ISectorPlantRepository, CrossCutting.WorkflowCore.Repositories.ISectorPlantRepository
    {

        private readonly IServiceProvider _serviceProvider;

        public SectorPlantRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Domain.SectorPlant GetOne(int idPlant, int idSector)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var sectorPlant = _ctx.SectorsPlants.Where(x => x.PlantID == idPlant && x.SectorID == idSector).FirstOrDefault();

                return _mapper.Map<SectorsPlants, Domain.SectorPlant>(sectorPlant);
            }


        }

        public List<string> GetSectorPlantReferredEmail(int plantId, int sectorId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {

                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var sectorPlant = _ctx.SectorsPlants.Where(x => x.PlantID == plantId && x.SectorID == sectorId)
                        .Include(x => x.JobsSectorsPlants)
                            .ThenInclude(x => x.Users)
                        .FirstOrDefault();
                var usersReferredSectorPlant = sectorPlant.JobsSectorsPlants
                        .Where(x => x.JobID == sectorPlant.ReferringJob || x.JobID == sectorPlant.ReferringJob2)
                        .Select(x => x.Users)
                        .ToList();

                List<string> emailsUser = new List<string>();

                foreach (var users in usersReferredSectorPlant)
                {
                    foreach (var user in users)
                    {
                        var alertsUsers = _ctx.AlertUsers.Where(x => x.UsersID == user.Id && x.GenerateAlert && (x.AlertID == 25 || x.AlertID == 28));

                        foreach (var item in alertsUsers)
                        {
                            emailsUser.Add(item.Users.Email);
                        }
                    }
                }

                //usersReferredSectorPlant
                //    .ForEach(jobSectorPlant => jobSectorPlant
                //            .Select(user => user.Email)
                //            .ToList()
                //            .ForEach(email => emailsUser.Add(email)));

                return emailsUser;
            }

        }
    }
}
