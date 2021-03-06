﻿using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDJob;
using Hoshin.Core.Application.UseCases.CRUDJob.UpdateJobUseCase;
using Hoshin.Core.Domain.Job;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Jobs
{
    public class UpdateJobTest
    {
        [Fact]
        public void UpdateJobReturnsUpdatedJobTest()
        {
            //Arrange
            Job jR = new Job();
            Job j = new Job(1, "Job 1", "jo1", true);
            JobOutput jO = new JobOutput() { JobId = 1, Name = "Job 1", Code = "jo1", Active = true };

            var mockJobRepository = new Mock<IJobRepository>();
            var mockMapper = new Mock<IMapper>();

            mockJobRepository.Setup(e => e.Update(It.IsAny<Job>())).Returns(j);
            mockMapper.Setup(e => e.Map<Job, JobOutput>(It.IsAny<Job>())).Returns(jO);

            var useCase = new UpdateJobUseCase(mockMapper.Object, mockJobRepository.Object);

            //Act
            var res = useCase.Execute(jR);

            //Assert
            Assert.IsType<JobOutput>(res);
        }

        [Fact]
        public void UpdateJobThrowsJobWithThisNameAndOrCodeAlreadyExistsExceptionTest()
        {
            //Arrange
            Job j = new Job(1, "Job 1", "jo1", true);

            var mockJobRepository = new Mock<IJobRepository>();
            var mockMapper = new Mock<IMapper>();

            mockJobRepository.Setup(e => e.CheckDuplicated(It.IsAny<Job>())).Returns(j);

            var useCase = new UpdateJobUseCase(mockMapper.Object, mockJobRepository.Object);

            //Act

            //Assert
            Assert.Throws<Application.Exceptions.Job.JobWithThisNameAndOrCodeAlreadyExists>(() => useCase.Execute(j));
        }
    }
}
