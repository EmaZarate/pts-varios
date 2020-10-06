using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Application.UseCases.AspectStates;
using Hoshin.Quality.Application.UseCases.AspectStates.GetAllAspectStates;
using Hoshin.Quality.Domain.AspectStates;
using Moq;
using Hoshin.Quality.Application.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.AspectStates.GetAllAspectStates;
using Hoshin.Quality.Application.UseCases.AspectStates.GetOneAspectState;
using Hoshin.Quality.Domain.FindingsState;
using Moq;
using Xunit;
using Hoshin.Quality.Application.Repositories;
using AutoMapper;

namespace Hoshin.Quality.UseCases.Test.AspectStates
{
    public class GetAspectStatesTest
    {
        [Fact]
        public async void GetAllFindingsStatesTest()
        {
            //Arrange
            List<Hoshin.Quality.Domain.AspectStates.AspectStates> list = new List<Hoshin.Quality.Domain.AspectStates.AspectStates>();
            list.Add(new Hoshin.Quality.Domain.AspectStates.AspectStates(1, "name1", "colour1", true));
            list.Add(new Hoshin.Quality.Domain.AspectStates.AspectStates(1, "name2", "colour2", true));
            List<AspectStatesOutput> aspectOutputList = new List<AspectStatesOutput>();
            AspectStatesOutput aspectStateOutput1 = new AspectStatesOutput();
            aspectStateOutput1.ID = 1;
            aspectStateOutput1.Name = "Estado1";
            aspectStateOutput1.Active = true;
            aspectStateOutput1.Colour = "colour1";
            AspectStatesOutput aspectStateOutput2 = new AspectStatesOutput();
            aspectStateOutput2.ID = 1;
            aspectStateOutput2.Name = "Estado1";
            aspectStateOutput2.Active = true;
            aspectStateOutput2.Colour = "colour1";
            aspectOutputList.Add(aspectStateOutput1);
            aspectOutputList.Add(aspectStateOutput2);

            var mockAspectStatesRepository = new Mock<IAspectStatesRepository>();
            var mockMapper = new Mock<IMapper>();
            mockAspectStatesRepository.Setup(e => e.GetAll()).Returns(list);

            mockMapper.Setup(e => e.Map<List<Hoshin.Quality.Domain.AspectStates.AspectStates>,  List<AspectStatesOutput> > (It.IsAny<List<Domain.AspectStates.AspectStates>>())).Returns(aspectOutputList);
      

            var useCase = new GetAllAspectStatesUseCase(mockAspectStatesRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute();

            //Assert
            Assert.Equal(2, res.Count);
        }

        [Fact]
        public async void GetOneAspectStatesWhenExistsTest()
        {
            //Arrange
            var aspectDomian = new Hoshin.Quality.Domain.AspectStates.AspectStates(1, "name1", "colour1", true);

            AspectStatesOutput aspectStateOutput1 = new AspectStatesOutput();
            aspectStateOutput1.ID = 1;
            aspectStateOutput1.Name = "Estado1";
            aspectStateOutput1.Active = true;
            aspectStateOutput1.Colour = "colour1";

            var mockAspectStatesRepository = new Mock<IAspectStatesRepository>();
            mockAspectStatesRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns(aspectDomian);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<Hoshin.Quality.Domain.AspectStates.AspectStates, AspectStatesOutput>(It.IsAny<Domain.AspectStates.AspectStates>())).Returns(aspectStateOutput1);

            var useCase = new GetOneAspectStateUseCase(mockMapper.Object, mockAspectStatesRepository.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<AspectStatesOutput>(res);
        }
        [Fact]
        public async void GetOneAspectStatesWhenNoExistsTest()
        {
            //Arrange
            var mockAspectStatesRepository = new Mock<IAspectStatesRepository>();
            mockAspectStatesRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns<Domain.AspectStates.AspectStates>(null);
            var mockMapper = new Mock<IMapper>();
            //mockMapper.Setup(e => e.Map<Domain.AspectStates.AspectStates, AspectStatesOutput>(It.IsAny<Domain.AspectStates.AspectStates>())).Returns(aspectStateOutput1);

            var useCase = new GetOneAspectStateUseCase(mockMapper.Object, mockAspectStatesRepository.Object);

            //Act

            //Assert
            Assert.Throws<EntityNotFoundException>(() => useCase.Execute(1));
        }
    }
}
