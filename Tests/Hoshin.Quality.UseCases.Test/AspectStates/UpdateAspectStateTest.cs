using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.AspectStates;
using Hoshin.Quality.Application.UseCases.AspectStates.UpdateAspectStatus;
using Moq;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.AspectStates
{
    public class UpdateAspectStateTest
    {
        [Fact]
        public async void ExecuteWithExistingAspectStatesTest()
        {
            //Arrange
            var newAspectState = new Domain.AspectStates.AspectStates(1,"name1", "colour1", true);
            var newAspectState2 = new Domain.AspectStates.AspectStates(1, "name1", "colour1", true);
            AspectStatesOutput aspectStateOutput1 = new AspectStatesOutput();
            aspectStateOutput1.ID = 1;
            aspectStateOutput1.Name = "Estado1";
            aspectStateOutput1.Active = true;
            aspectStateOutput1.Colour = "colour1";
            var mockAspectStatesRepository = new Mock<IAspectStatesRepository>();
            mockAspectStatesRepository.Setup(e => e.Update(It.IsNotNull<Domain.AspectStates.AspectStates>())).Returns(newAspectState);
            mockAspectStatesRepository.Setup(e => e.Get(It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(newAspectState);
            mockAspectStatesRepository.Setup(e => e.GetOne(It.IsNotNull<int>())).Returns(newAspectState2);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<Hoshin.Quality.Domain.AspectStates.AspectStates, AspectStatesOutput>(It.IsAny<Domain.AspectStates.AspectStates>())).Returns(aspectStateOutput1);
            UpdateAspectStatusUseCase useCase = new UpdateAspectStatusUseCase(mockMapper.Object, mockAspectStatesRepository.Object);

            //Act
            var res = useCase.Execute(1,"name1", "colour1", true);

            //Assert
            Assert.IsType<AspectStatesOutput>(res);
        }

        [Fact]
        public async void ExecuteWithNoExistingAspectStatesTest()
        {
            //Arrange
            var mockAspectStatesRepository = new Mock<IAspectStatesRepository>();
            mockAspectStatesRepository.Setup(e => e.Update(It.IsNotNull<Domain.AspectStates.AspectStates>())).Returns<AspectStatesOutput>(null);
            var mockMapper = new Mock<IMapper>();
            UpdateAspectStatusUseCase useCase = new UpdateAspectStatusUseCase(mockMapper.Object, mockAspectStatesRepository.Object);

            //Act

            //Assert
            Assert.Throws<EntityNotFoundException>(() => useCase.Execute(1, "name1", "colour1", true));
        }

        [Fact]
        public async void ExecuteWithDuplicateAspectStatesTest()
        {
            //Arrange
            var newAspectState = new Domain.AspectStates.AspectStates(1,"name1", "colour1", true);
            var newAspectState2 = new Domain.AspectStates.AspectStates(2,"name1", "colour1", true);
            AspectStatesOutput aspectStateOutput1 = new AspectStatesOutput();
            aspectStateOutput1.ID = 1;
            aspectStateOutput1.Name = "Estado1";
            aspectStateOutput1.Active = true;
            aspectStateOutput1.Colour = "colour1";
            var mockAspectStatesRepository = new Mock<IAspectStatesRepository>();
            mockAspectStatesRepository.Setup(e => e.CheckExistsAspectState(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<int>())).Returns(newAspectState);
            mockAspectStatesRepository.Setup(e => e.GetOne(It.IsNotNull<int>())).Returns(newAspectState2);
           
            var mockMapper = new Mock<IMapper>();
           
            UpdateAspectStatusUseCase useCase = new UpdateAspectStatusUseCase(mockMapper.Object, mockAspectStatesRepository.Object);

            //Act


            //Assert
            Assert.Throws<DuplicateEntityException>(() => useCase.Execute(1, "name1", "colour1", true));
        }
    }
}
