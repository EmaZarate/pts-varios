using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.UpdateParametrizationCriteria;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.ParametrizationCriteria
{
    public class UpdateParametrizationCriteriaTest
    {
        [Fact]
        public async void ExecuteWithExistingParametrizationCriteriaTest()
        {
            //Arrange
            var paramC = new Domain.ParametrizationCriteria.ParametrizationCriteria("test", "test", 1);
            var mockParametrizationCriteriaRepository = new Mock<IParametrizationCriteriaRepository>();
            mockParametrizationCriteriaRepository.Setup(e => e.Get(It.IsNotNull<int>())).Returns(paramC);
            mockParametrizationCriteriaRepository.Setup(e => e.Update(It.IsNotNull<Domain.ParametrizationCriteria.ParametrizationCriteria>())).Returns(paramC);

            var useCase = new UpdateParametrizationCriteriaUseCase(mockParametrizationCriteriaRepository.Object);
            //Act

            var res = useCase.Execute(1, "test","test");

            //Assert
            Assert.IsType<ParametrizationCriteriaOutput>(res);
        }
        [Fact]
        public async void ExecuteWithNoExistingParametrizationCriteriaTest()
        {
            //Arrange
            var paramC = new Domain.ParametrizationCriteria.ParametrizationCriteria("test", "test", 1);
            var mockParametrizationCriteriaRepository = new Mock<IParametrizationCriteriaRepository>();
            mockParametrizationCriteriaRepository.Setup(e => e.Get(It.IsNotNull<string>())).Returns(paramC);
            mockParametrizationCriteriaRepository.Setup(e => e.Update(It.IsNotNull<Domain.ParametrizationCriteria.ParametrizationCriteria>())).Returns(paramC);

            var useCase = new UpdateParametrizationCriteriaUseCase(mockParametrizationCriteriaRepository.Object);
            //Act
            //Assert
            Assert.Throws<EntityNotFoundException>(() => useCase.Execute(1,"test", "test"));
        }
    }
}
