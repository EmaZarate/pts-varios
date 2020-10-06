using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.UpdateCorrectiveActionState;
using Hoshin.Quality.Domain.CorrectiveActionState;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.CorrectiveActionStates
{
    public class UpdateCorrectiveActionStateTest
    {
        [Fact]
        public void ExecuteUpdateCorrectiveActionStateTest()
        {
            //Arrange
            var state = new CorrectiveActionState()
            {
                CorrectiveActionStateID = 1,
                Code = "PRB",
                Name = "Prueba",
                Color = "eaeaea",
                Active = true
            };

            var mockCorrectiveActionStateRepository = new Mock<ICorrectiveActionStateRepository>();
            mockCorrectiveActionStateRepository.Setup(e => e.Update(It.IsAny<CorrectiveActionState>())).Returns(state);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<CorrectiveActionState, CorrectiveActionStateOutput>(It.IsAny<CorrectiveActionState>())).Returns(new CorrectiveActionStateOutput());

            var useCase = new UpdateCorrectiveActionStateUseCase(mockCorrectiveActionStateRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(state);

            //Assert
            Assert.IsType<CorrectiveActionStateOutput>(res);
        }
    }
}
