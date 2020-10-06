using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FindingsStates;
using Hoshin.Quality.Application.UseCases.FindingsStates.UpdateFindingsStates;
using Hoshin.Quality.Domain.FindingsState;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.FindingsStates
{
    public class UpdateFindingsStatesTest
    {
        [Fact]
        public async void ExecuteWithExistingFindingsStatesTest()
        {
            //Arrange
            var findingsS = new FindingsState("code1","name1","colour1",true,1);
            var mockFindingsStatesCriteriaRepository = new Mock<IFindingStateRepository>();
            mockFindingsStatesCriteriaRepository.Setup( e=> e.Get(It.IsNotNull<int>())).Returns(findingsS);
            mockFindingsStatesCriteriaRepository.Setup(e => e.Update(It.IsNotNull<FindingsState>())).Returns(findingsS);

            var useCase = new UpdateFindingsStatesUseCase(mockFindingsStatesCriteriaRepository.Object);
            //Act

            var res = useCase.Execute(1, "code1", "name1", "colour1", true);

            //Assert
            Assert.IsType<FindingsStatesOutput>(res);
        }

        [Fact]
        public async void ExecuteWithNoExistingFindingsStatesTest()
        {
            //Arrange
            var findingsS = new FindingsState("code1", "name1", "colour1", true, 1);
            var mockFindingsStatesCriteriaRepository = new Mock<IFindingStateRepository>();
            mockFindingsStatesCriteriaRepository.Setup(e => e.Get(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(findingsS);
            mockFindingsStatesCriteriaRepository.Setup(e => e.Update(It.IsNotNull<FindingsState>())).Returns(findingsS);

            var useCase = new UpdateFindingsStatesUseCase(mockFindingsStatesCriteriaRepository.Object);

            //Act

            //Assert
            Assert.Throws<EntityNotFoundException>(() => useCase.Execute(1, "code1", "name1", "colour1", true));
        }

        [Fact]
        public async void ExecuteWithDuplicateValuesFindingsStatesTest()
        {
            //Arrange
            var findingsS = new FindingsState("code1", "name1", "colour1", true, 1);
            var findingsS2 = new FindingsState("code1", "name1", "colour1", true, 2);
            var mockFindingsStatesCriteriaRepository = new Mock<IFindingStateRepository>();
            mockFindingsStatesCriteriaRepository.Setup(e => e.Get(It.IsNotNull<int>())).Returns(findingsS2);
            mockFindingsStatesCriteriaRepository.Setup(e => e.CheckExistsFindingState(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<int>())).Returns(findingsS);
            mockFindingsStatesCriteriaRepository.Setup(e => e.Update(It.IsNotNull<FindingsState>())).Returns(findingsS);

            var useCase = new UpdateFindingsStatesUseCase(mockFindingsStatesCriteriaRepository.Object);
            //Act

            //Assert
            Assert.Throws<DuplicateEntityException>(() => useCase.Execute(1, "code1", "name1", "colour1", true));
        }
    }
}
