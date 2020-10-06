
using AutoMapper;
using Hoshin.Core.Application.Exceptions.Plant;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Domain.Plant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class PlantRepository : IPlantRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;
        public PlantRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<List<Plant>> GetAll()
        {
            var list = await _ctx.Plants
                        .Include(x => x.SectorsPlants)
                            .ThenInclude(y => y.Sector)
                        .Include(x => x.SectorsPlants)
                            .ThenInclude(y => y.JobsSectorsPlants)
                                .ThenInclude(z => z.Job)
                            .ToListAsync();
            return _mapper.Map<List<Plants>, List<Plant>>(list);
        }

        public Plant GetOne(int id)
        {
            return _mapper.Map<Plants, Plant>(_ctx.Plants.Find(id));
        }

        public Plant Add(Plant plant)
        {            
            Plants plantDb = _mapper.Map<Plant, Plants>(plant);
            _ctx.Plants.Add(plantDb);
            _ctx.SaveChanges();
            return _mapper.Map<Plants, Plant>(plantDb);
        }

        public Plant Update(Plant plant)
        {
            Plants plantDb = _mapper.Map<Plant, Plants>(plant);
            _ctx.Plants.Update(plantDb);
            _ctx.SaveChanges();
            return _mapper.Map<Plants, Plant>(plantDb);            
        }

        public Plant CheckDuplicated(Plant plant)
        {
            return _mapper.Map<Plants, Plant>(_ctx.Plants.Where(x => x.Name == plant.Name && x.Country == plant.Country && x.PlantID != plant.PlantID).FirstOrDefault());
        }

        public void UpdateAssociations(Plant plant)
        {
            try
            {
                var pl = _ctx.Plants
            .Include(x => x.SectorsPlants)
                .ThenInclude(x => x.JobsSectorsPlants)
            .Where(x => x.PlantID == plant.PlantID)
            .FirstOrDefault();

                var listSectorsPlants = new List<SectorsPlants>();
                foreach (var sector in plant.Sectors)
                {
                    var sectorPlant = pl.SectorsPlants.Where(x => x.SectorID == sector.Id && x.PlantID == plant.PlantID).FirstOrDefault();
                    if (sectorPlant == null)
                    {
                        sectorPlant = new SectorsPlants()
                        {
                            Plant = pl,
                            Sector = _ctx.Sectors.Where(x => x.SectorID == sector.Id).FirstOrDefault(),
                            JobsSectorsPlants = new List<JobsSectorsPlants>()
                        };
                    }
                    if (sector.jobs != null && sector.jobs.Count > 0)
                    {
                        sectorPlant.JobsSectorsPlants = this.GetUpdatedJobsOfSector(sector.jobs, sectorPlant);
                    }
                    listSectorsPlants.Add(sectorPlant);
                }
                pl.SectorsPlants = listSectorsPlants;
                _ctx.Update(pl);
                _ctx.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("No puede actualizarse ya que existen registros asociados a la Planta/Sector/Puesto", ex);
            }
            catch(Exception ex)
            {
                throw new Exception("No puede actualizarse ya que existen registros asociados a la Planta/Sector/Puesto", ex);
            }
        }

        public ICollection<JobsSectorsPlants> GetUpdatedJobsOfSector(List<Domain.Job.Job> jobs, SectorsPlants sectorsPlants)
        {
            var jobsId = jobs.Select(y => y.Id).ToList();
            var existingJobs = sectorsPlants.JobsSectorsPlants.Where(x => jobsId.Contains(x.JobID));
            var jobsToAdd = jobsId.Except(existingJobs.Select(x => x.JobID));

            var jobsAdded = new List<JobsSectorsPlants>();

            foreach (var job in jobsToAdd)
            {
                var jobSectorPlant = new JobsSectorsPlants
                {
                    Job = _ctx.Jobs.Where(x => x.JobID == job).FirstOrDefault(),
                    SectorPlant = sectorsPlants
                };
                jobsAdded.Add(jobSectorPlant);
            }

            var allJobsOfSector = jobsAdded.Union(existingJobs);

            return allJobsOfSector.ToList();
        }
    }
}
