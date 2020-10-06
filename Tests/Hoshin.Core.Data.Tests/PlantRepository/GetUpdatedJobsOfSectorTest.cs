using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Hoshin.Core.Data.Tests.PlantRepository
{

    public class GetUpdatedJobsOfSectorTest
    {
        List<Domain.Job.Job> jobs;
        SectorsPlants sectorsPlants;
        List<Jobs> jobsDbSet;
        private readonly SQLHoshinCoreContext _ctx;
        public GetUpdatedJobsOfSectorTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SQLHoshinCoreContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            _ctx = new SQLHoshinCoreContext(optionsBuilder);
            
            jobs = new List<Domain.Job.Job>();
            sectorsPlants = new SectorsPlants();
            jobsDbSet = new List<Jobs>
            {
                new Jobs { JobID = 1 },
                new Jobs { JobID = 2 },
                new Jobs { JobID = 3 },
                new Jobs { JobID = 4 },
                new Jobs { JobID = 5 },
                new Jobs { JobID = 6 },
                new Jobs { JobID = 7 },
                new Jobs { JobID = 8 },
                new Jobs { JobID = 9 },
                new Jobs { JobID = 10 }
            };

            _ctx.Jobs.AddRange(jobsDbSet);
            _ctx.SaveChanges();

            sectorsPlants.JobsSectorsPlants = new List<JobsSectorsPlants>();
            sectorsPlants.JobsSectorsPlants.Add(new JobsSectorsPlants
            {
                PlantID = 1,
                SectorID = 1,
                JobID = 1,
            });

            sectorsPlants.JobsSectorsPlants.Add(new JobsSectorsPlants
            {
                PlantID = 1,
                SectorID = 1,
                JobID = 2,
            });

            sectorsPlants.JobsSectorsPlants.Add(new JobsSectorsPlants
            {
                PlantID = 1,
                SectorID = 1,
                JobID = 3,
            });

            sectorsPlants.JobsSectorsPlants.Add(new JobsSectorsPlants
            {
                PlantID = 1,
                SectorID = 1,
                JobID = 4,
            });
        }
        [Fact]
        public void GetListJobsOfSectorWhenOnlyAddJobsToSector()
        {
            //Arrange
            jobs.Add(new Domain.Job.Job
            {
                Id = 1,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 2,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 3,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 4,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 5,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 6,
                Active = true
            });
            var mockMapper = new Mock<IMapper>();
            var repository = new Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.PlantRepository(_ctx, mockMapper.Object);

            //Act
            var result = repository.GetUpdatedJobsOfSector(jobs, sectorsPlants);

            //Assert
            Assert.Equal(6, result.ToList().Count);
        }

        [Fact]
        public void GetListJobsOfSectorWhenAddAndDeleteJobsToSector()
        {
            //Arrange
            jobs.Add(new Domain.Job.Job
            {
                Id = 1,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 5,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 6,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 7,
                Active = true
            });
            var mockMapper = new Mock<IMapper>();
            var repository = new Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.PlantRepository(_ctx, mockMapper.Object);

            //Act
            var result = repository.GetUpdatedJobsOfSector(jobs, sectorsPlants);

            //Assert
            Assert.Equal(4, result.ToList().Count);
        }

        [Fact]
        public void GetListJobsOfSectorWhenOnlyDeleteJobsToSector()
        {
            //Arrange
            jobs.Add(new Domain.Job.Job
            {
                Id = 1,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 2,
                Active = true
            });
            var mockMapper = new Mock<IMapper>();
            var repository = new Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.PlantRepository(_ctx, mockMapper.Object);

            //Act
            var result = repository.GetUpdatedJobsOfSector(jobs, sectorsPlants);

            //Assert
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void GetListJobsOfSectorWhenNoChangesJobs()
        {
            jobs.Add(new Domain.Job.Job
            {
                Id = 1,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 2,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 3,
                Active = true
            });
            jobs.Add(new Domain.Job.Job
            {
                Id = 4,
                Active = true
            });
            var mockMapper = new Mock<IMapper>();
            var repository = new Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.PlantRepository(_ctx, mockMapper.Object);

            //Act
            var result = repository.GetUpdatedJobsOfSector(jobs, sectorsPlants);

            //Assert
            Assert.Equal(4, result.ToList().Count);
        }
    }
}
