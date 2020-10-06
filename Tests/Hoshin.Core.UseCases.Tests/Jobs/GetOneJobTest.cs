using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDJob;
using Hoshin.Core.Application.UseCases.CRUDJob.GetOneJobUseCase;
using Hoshin.Core.Domain.Job;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Jobs
{
    public class GetOneJobTest
    {
        [Fact]
        public void GetOneJobReturnsNullTest()
        {
            //Arrange
            Job j = new Job();

            var mockJobRepository = new Mock<IJobRepository>();
            var mockMapper = new Mock<IMapper>();

            mockJobRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns<JobOutput>(null);
            mockMapper.Setup(e => e.Map<Job, JobOutput>(It.IsAny<Job>())).Returns<JobOutput>(null);

            var useCase = new GetOneJobUseCase(mockMapper.Object, mockJobRepository.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public void GetOneJobReturnsSuccesfullyTest()
        {
            //Arrange
            Job j = new Job(1, "Job 1", "jo1", true);
            JobOutput jO = new JobOutput { JobId = 1, Name = "Job 1", Code = "jo1", Active = true };

            var mockJobRepository = new Mock<IJobRepository>();
            var mockMapper = new Mock<IMapper>();

            mockJobRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns(j);
            mockMapper.Setup(e => e.Map<Job, JobOutput>(It.IsAny<Job>())).Returns(jO);

            var useCase = new GetOneJobUseCase(mockMapper.Object, mockJobRepository.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<JobOutput>(res);
        }
    }
}
