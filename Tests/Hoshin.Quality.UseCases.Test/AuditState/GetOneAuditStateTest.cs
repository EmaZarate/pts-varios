using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.AuditState;
using Hoshin.Quality.Application.UseCases.AuditState.GetOneAuditState;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.AuditState
{
    public class GetOneAuditStateTest
    {
        [Fact]
        public async void GetOneAuditStateByIdTest()
        {
            //Arrange
            var param = new Domain.AuditState.AuditState() {
                AuditStateID = 1,
                Active = true,
                Code = "Code 2",
                Name = "Programada",
                Color = "#FFCC22"
            };

            var mockAuditStatesRepository = new Mock<IAuditStateRepository>();
            mockAuditStatesRepository.Setup(e => e.Get(It.IsAny<int>())).Returns(param);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<Domain.AuditState.AuditState, AuditStateOutput>(It.IsAny<Domain.AuditState.AuditState>())).Returns(new AuditStateOutput());
            var useCase = new GetOneAuditStateUseCase(mockAuditStatesRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<AuditStateOutput>(res);
        }
    }
}
