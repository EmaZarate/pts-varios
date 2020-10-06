using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.CreateParametrizationCriteria;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.ParametrizationCriteria
{
    public class CreateParametrizationCriteriaTest
    {
        [Fact]
        public void ExecuteWithNewParametrizationCriteriaTest()
        {
            //Arrange
            var paramC = new Domain.ParametrizationCriteria.ParametrizationCriteria("test", "test",1);
            var mockParametrizationCriteriaRepository = new Mock<IParametrizationCriteriaRepository>();
            mockParametrizationCriteriaRepository.Setup(e => e.Get(It.IsNotNull<string>())).Returns<Domain.ParametrizationCriteria.ParametrizationCriteria>(null);
            mockParametrizationCriteriaRepository.Setup(e => e.Add(It.IsNotNull<Domain.ParametrizationCriteria.ParametrizationCriteria>())).Returns(paramC);

            CreateParametrizationCriteriaUseCase createParametrizationCriteriaUseCase = new CreateParametrizationCriteriaUseCase(mockParametrizationCriteriaRepository.Object);
            //Act

            var res = createParametrizationCriteriaUseCase.Execute("test", "test");

            //Assert
            Assert.IsType<ParametrizationCriteriaOutput>(res);
        }
        [Fact]
        public void ExecuteWithDuplicateParametrizationCriteriaTest()
        {
            //Arrange
            var paramC = new Domain.ParametrizationCriteria.ParametrizationCriteria("test", "test");
            var mockParametrizationCriteriaRepository = new Mock<IParametrizationCriteriaRepository>();
            mockParametrizationCriteriaRepository.Setup(e => e.Get(It.IsNotNull<string>())).Returns(paramC);

            CreateParametrizationCriteriaUseCase createParametrizationCriteriaUseCase = new CreateParametrizationCriteriaUseCase(mockParametrizationCriteriaRepository.Object);
            //Act


            //Assert
            Assert.Throws<DuplicateEntityException>(() => createParametrizationCriteriaUseCase.Execute("test", "test"));
        }
    }
}
