using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FindingsStates;
using Hoshin.Quality.Application.UseCases.FindingsStates.CreateFindingsStateUseCase;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.FindingsStates
{
    public class CreateFindingsStatesTest
    {
        [Fact]
        public void ExecuteWithNewFindingsStatesTest()
        {
            //Arrange
            var newParam = new Domain.FindingsState.FindingsState("code1", "name1", "colour1", true);
            var mockFindingsStatesRepository = new Mock<IFindingStateRepository>();
            mockFindingsStatesRepository.Setup(e => e.Get(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<string>())).Returns<Domain.FindingsState.FindingsState>(null);
            mockFindingsStatesRepository.Setup(e => e.Add(It.IsNotNull<Domain.FindingsState.FindingsState>())).Returns(newParam);

            CreateFindingsStateUseCase useCase = new CreateFindingsStateUseCase(mockFindingsStatesRepository.Object);

            //Act
            var res = useCase.Execute("code1", "name1", "colour1", true);

            //Assert
            Assert.IsType<FindingsStatesOutput>(res);
        }

        [Fact]
        public void ExecuteWithDuplicateFindingsStatesTest()
        {
            //Arrange
            var paramC = new Domain.FindingsState.FindingsState("code1", "name1", "colour1", true);
            var mockFindingsStatesRepository = new Mock<IFindingStateRepository>();
            mockFindingsStatesRepository.Setup(e => e.Get(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(paramC);

            CreateFindingsStateUseCase useCase = new CreateFindingsStateUseCase(mockFindingsStatesRepository.Object);

            //Act

            //Assert
            Assert.Throws<DuplicateEntityException>(() => useCase.Execute("code1", "name1", "colour1", true));
        }
    }
}
