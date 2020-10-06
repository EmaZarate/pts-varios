using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.AuditState;
using Hoshin.Quality.Application.UseCases.AuditState.CreateAuditState;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.AuditState
{
    public class CreateAuditStateTest
    {
        [Fact]
        public void ExecuteWithNewAuditStateTest()
        {
            //Arrange
            var newParam = new Domain.AuditState.AuditState() {
                Active = true,
                Code = "Code 2",
                Color = "#FFCC22",
                Name = "Programada"
            };

            var mockAuditStateRepository = new Mock<IAuditStateRepository>();
            var mockMapper = new Mock<IMapper>();
            mockAuditStateRepository.Setup(e => e.Add(It.IsNotNull<Domain.AuditState.AuditState>())).Returns(newParam);

            CreateAuditStateUseCase useCase = new CreateAuditStateUseCase(mockAuditStateRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(newParam);

            //Assert
            Assert.IsType<AuditStateOutput>(res);
        }
    }
}
