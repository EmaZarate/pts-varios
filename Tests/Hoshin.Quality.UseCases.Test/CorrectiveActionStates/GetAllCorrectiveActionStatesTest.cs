using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetAllCorrectiveActionStates;
using Hoshin.Quality.Domain.CorrectiveActionState;
using Moq;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.CorrectiveActionStates
{
    public class GetAllCorrectiveActionStatesTest
    {
        [Fact]
        public void ExecuteGetAllCorrectiveActionStatesTest()
        {
            //Arrange
            var mockCorrectiveActionStateRepository = new Mock<ICorrectiveActionStateRepository>();
            mockCorrectiveActionStateRepository.Setup(e => e.GetAll()).Returns(new List<CorrectiveActionState>());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<List<CorrectiveActionState>, List<CorrectiveActionStateOutput>>(It.IsAny<List<CorrectiveActionState>>())).Returns(new List<CorrectiveActionStateOutput>());

            var useCase = new GetAllCorrectiveActionStatesUseCase(mockCorrectiveActionStateRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute();

            //Assert
            Assert.IsType<List<CorrectiveActionStateOutput>>(res);
        }
    }
}
