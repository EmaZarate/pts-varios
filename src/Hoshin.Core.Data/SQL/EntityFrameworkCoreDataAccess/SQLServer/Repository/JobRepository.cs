using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Domain.Job;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;

        public JobRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<List<Job>> GetAll()
        {
            var list = await _ctx.Jobs
                        .Include(x => x.JobsSectorsPlants)
                        .ToListAsync();

            return _mapper.Map<List<Jobs>, List<Job>>(list);
        }

        public Job GetOne(int id)
        {
            return _mapper.Map<Jobs, Job>(_ctx.Jobs.Find(id));
        }

        public Job Add(Job job)
        {
            Jobs jobDb = _mapper.Map<Job, Jobs>(job);
            _ctx.Jobs.Add(jobDb);
            _ctx.SaveChanges();

            return _mapper.Map<Jobs, Job>(jobDb);
        }

        public Job Update(Job job)
        {
            Jobs jobDb = _mapper.Map<Job, Jobs>(job);
            _ctx.Jobs.Update(jobDb);
            _ctx.SaveChanges();

            return _mapper.Map<Jobs, Job>(jobDb);
        }

        public Job CheckDuplicated(Job job)
        {
            return _mapper.Map<Jobs, Job>(_ctx.Jobs.Where(x => (x.Code == job.Code || x.JobTitle == job.Name) && x.JobID != job.Id).FirstOrDefault());
        }
    }
}
