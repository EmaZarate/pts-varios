using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.AspectStates;
using Hoshin.Quality.Application.UseCases.AspectStates.CreateAspectState;
using Moq;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.AspectStates
{
    public class CreateAspectStateTest
    {
        [Fact]
        public void AddAspectStateTest()
        {
            //Arrange
            var newAspectState = new Domain.AspectStates.AspectStates("name1", "colour1", true);
            AspectStatesOutput aspectStateOutput1 = new AspectStatesOutput();
            aspectStateOutput1.ID = 1;
            aspectStateOutput1.Name = "Estado1";
            aspectStateOutput1.Active = true;
            aspectStateOutput1.Colour = "colour1";
            var mockAspectStatesRepository = new Mock<IAspectStatesRepository>();
            mockAspectStatesRepository.Setup(e => e.Get(It.IsNotNull<string>(), It.IsNotNull<string>())).Returns<Domain.AspectStates.AspectStates> (null);
            mockAspectStatesRepository.Setup(e => e.Add(It.IsNotNull<Domain.AspectStates.AspectStates>())).Returns(newAspectState);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<Hoshin.Quality.Domain.AspectStates.AspectStates, AspectStatesOutput>(It.IsAny<Domain.AspectStates.AspectStates>())).Returns(aspectStateOutput1);
            CreateAspectStateUseCase useCase = new CreateAspectStateUseCase(mockMapper.Object, mockAspectStatesRepository.Object);

            //Act
            var res = useCase.Execute("name1", "colour1", true);

            //Assert
            Assert.IsType<AspectStatesOutput>(res);
        }
        [Fact]
        public void AddDuplicateAspectStateTest()
        {
            //Arrange
            var newAspectState = new Domain.AspectStates.AspectStates("name1", "colour1", true);
            AspectStatesOutput aspectStateOutput1 = new AspectStatesOutput();
            aspectStateOutput1.ID = 1;
            aspectStateOutput1.Name = "Estado1";
            aspectStateOutput1.Active = true;
            aspectStateOutput1.Colour = "colour1";
            var mockAspectStatesRepository = new Mock<IAspectStatesRepository>();
            mockAspectStatesRepository.Setup(e => e.Get(It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(newAspectState);
            mockAspectStatesRepository.Setup(e => e.Add(It.IsNotNull<Domain.AspectStates.AspectStates>())).Returns(newAspectState);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<Hoshin.Quality.Domain.AspectStates.AspectStates, AspectStatesOutput>(It.IsAny<Domain.AspectStates.AspectStates>())).Returns(aspectStateOutput1);
            CreateAspectStateUseCase useCase = new CreateAspectStateUseCase(mockMapper.Object, mockAspectStatesRepository.Object);

            //Act


            //Assert
            Assert.Throws<DuplicateEntityException>(() => useCase.Execute("name1", "colour1", true));
        }
    }
}
