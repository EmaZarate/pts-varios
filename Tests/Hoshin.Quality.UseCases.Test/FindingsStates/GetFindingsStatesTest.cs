using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FindingsStates;
using Hoshin.Quality.Application.UseCases.FindingsStates.GetAllFindingsStates;
using Hoshin.Quality.Application.UseCases.FindingsStates.GetOneFindingsStates;
using Hoshin.Quality.Domain.FindingsState;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.FindingsStates
{
    public class GetFindingsStatesTest
    {
        [Fact]
        public async void GetAllFindingsStatesTest()
        {
            //Arrange
            List<FindingsState> list = new List<FindingsState>();
            list.Add(new FindingsState("code1", "name1", "colour1", true));
            list.Add(new FindingsState("code2", "name2", "colour2", true));
            var mockFindingsStatesRepository = new Mock<IFindingStateRepository>();
            mockFindingsStatesRepository.Setup(e => e.GetAll()).Returns(list);

            var useCase = new GetAllFindingsStatesUseCase(mockFindingsStatesRepository.Object);

            //Act
            var res = useCase.Execute();

            //Assert
            Assert.Equal(2, res.Count);
        }

        [Fact]
        public async void GetOneFindingsStatesWhenExistsTest()
        {
            //Arrange
            var param = new FindingsState("code1", "name1", "colour1", true, 1);
            var mockFindingsStatesRepository = new Mock<IFindingStateRepository>();
            mockFindingsStatesRepository.Setup(e => e.Get(It.IsAny<int>())).Returns(param);

            var useCase = new GetOneFindingsStatesUseCase(mockFindingsStatesRepository.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<FindingsStatesOutput>(res);
        }

        [Fact]
        public async void GetOneFindingsStatesWhenNoExistsTest()
        {
            //Arrange
            var mockFindingsStatesRepository = new Mock<IFindingStateRepository>();
            mockFindingsStatesRepository.Setup(e => e.Get(It.IsAny<int>())).Returns<FindingsState>(null);

            var useCase = new GetOneFindingsStatesUseCase(mockFindingsStatesRepository.Object);

            //Act

            //Assert
            Assert.Throws<EntityNotFoundException>(() => useCase.Execute(1));
        }
    }
}
