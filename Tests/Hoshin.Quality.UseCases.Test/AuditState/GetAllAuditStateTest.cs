using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.AuditState;
using Hoshin.Quality.Application.UseCases.AuditState.GetAllAuditState;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.AuditState
{
    public class GetAllAuditStateTest
    {
        [Fact]
        public async void ExecuteGetAllAuditStateTest()
        {
            var mockAuditStatesRepository = new Mock<IAuditStateRepository>();
            mockAuditStatesRepository.Setup(e => e.GetAll()).Returns(new List<Domain.AuditState.AuditState>());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<List<Domain.AuditState.AuditState>, List<AuditStateOutput>>(It.IsAny<List<Domain.AuditState.AuditState>>())).Returns(new List<AuditStateOutput>());
            var useCase = new GetAllAuditStateUseCase(mockAuditStatesRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute();

            //Assert
            Assert.IsType<List<AuditStateOutput>>(res);
        }
    }
}
