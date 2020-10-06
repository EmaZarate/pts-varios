using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDJob;
using Hoshin.Core.Application.UseCases.CRUDJob.GetAllJobsUseCase;
using Hoshin.Core.Domain.Job;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Jobs
{
    public class GetAllJobsTest
    {
        [Fact]
        public async void GetAllJobsReturnsEmptyListTest()
        {
            //Arrange
            List<Job> list = new List<Job>();

            var mockJobRepository = new Mock<IJobRepository>();
            var mockMapper = new Mock<IMapper>();

            mockJobRepository.Setup(e => e.GetAll()).ReturnsAsync(list);
            mockMapper.Setup(e => e.Map<List<Job>, List<JobOutput>>(It.IsAny<List<Job>>())).Returns(new List<JobOutput>());

            var useCase = new GetAllJobsUseCase(mockMapper.Object, mockJobRepository.Object);

            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.Empty(res);
        }

        public async void GetAllJobsReturnsListWith3JobsTest()
        {
            //Arrange
            List<Job> list = new List<Job>();
            JobOutput j1 = new JobOutput { JobId = 1, Name = "Job 1", Code = "jo1", Active = true };
            JobOutput j2 = new JobOutput { JobId = 2, Name = "Job 2", Code = "jo2", Active = true };
            JobOutput j3 = new JobOutput { JobId = 3, Name = "Job 3", Code = "jo3", Active = true };

            var mockJobRepository = new Mock<IJobRepository>();
            var mockMapper = new Mock<IMapper>();

            mockJobRepository.Setup(e => e.GetAll()).ReturnsAsync(list);
            mockMapper.Setup(e => e.Map<List<Job>, List<JobOutput>>(It.IsAny<List<Job>>())).Returns(new List<JobOutput> { j1, j2, j3 });

            var useCase = new GetAllJobsUseCase(mockMapper.Object, mockJobRepository.Object);

            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.Equal(3, res.Count);
        }
    }
}
