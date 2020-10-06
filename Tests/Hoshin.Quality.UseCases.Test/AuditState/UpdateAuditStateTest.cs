using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.AuditState;
using Hoshin.Quality.Application.UseCases.AuditState.UpdateAuditState;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.AuditState
{
    public class UpdateAuditStateTest
    {
        [Fact]
        public void ExecuteWithUpdateAuditStateTest()
        {
            //Arrange
            var updateParam = new Domain.AuditState.AuditState()
            {
                Active = true,
                Code = "Code 2",
                Color = "#FFCC22",
                Name = "Programada"
            };

            var mockAuditStateRepository = new Mock<IAuditStateRepository>();
            mockAuditStateRepository.Setup(e => e.Update(It.IsNotNull<Domain.AuditState.AuditState>())).Returns(updateParam);
            mockAuditStateRepository.Setup(e => e.Get(It.IsNotNull<int>())).Returns(updateParam);

            UpdateAuditStateUseCase useCase = new UpdateAuditStateUseCase(mockAuditStateRepository.Object);

            //Act
            var res = useCase.Execute(updateParam);

            //Assert
            Assert.IsType<AuditStateOutput>(res);
        }
    }
}
