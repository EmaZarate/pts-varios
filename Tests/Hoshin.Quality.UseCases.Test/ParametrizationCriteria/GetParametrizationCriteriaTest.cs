using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetAllParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetOneParametrizationCriteria;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.ParametrizationCriteria
{
    public class GetParametrizationCriteriaTest
    {
        [Fact]
        public async void GetAllParametrizationCriteriaTest()
        {
            //Arrange
            List<Domain.ParametrizationCriteria.ParametrizationCriteria> list = new List<Domain.ParametrizationCriteria.ParametrizationCriteria>();
            list.Add(new Domain.ParametrizationCriteria.ParametrizationCriteria("obj1", "obj1"));
            list.Add(new Domain.ParametrizationCriteria.ParametrizationCriteria("obj2", "obj2"));
            var mockParametrizationCriteriaRepository = new Mock<IParametrizationCriteriaRepository>();
            mockParametrizationCriteriaRepository.Setup(e => e.GetAll()).Returns(list);

            var useCase = new GetAllParametrizationCriteriaUseCase(mockParametrizationCriteriaRepository.Object);
            //Act
            var res = useCase.Execute();

            //Assert
            Assert.Equal(2, res.Count);
        }

        [Fact]
        public async void GetOneParametrizationCriteriaWhenExistsTest()
        {
            //Arrange
            var param = new Domain.ParametrizationCriteria.ParametrizationCriteria("prueba", "int", 2);

            var mockParametrizationCriteriaRepository = new Mock<IParametrizationCriteriaRepository>();
            mockParametrizationCriteriaRepository.Setup(e => e.Get(It.IsAny<int>())).Returns(param);

            var useCase = new GetOneParametrizationCriteriaUseCase(mockParametrizationCriteriaRepository.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<ParametrizationCriteriaOutput>(res);
        }

        [Fact]
        public async void GetOneParametrizationCriteriaWhenNoExistsTest()
        {
            //Arrange

            var mockParametrizationCriteriaRepository = new Mock<IParametrizationCriteriaRepository>();
            mockParametrizationCriteriaRepository.Setup(e => e.Get(It.IsAny<int>())).Returns<Domain.ParametrizationCriteria.ParametrizationCriteria>(null);

            var useCase = new GetOneParametrizationCriteriaUseCase(mockParametrizationCriteriaRepository.Object);

            //Act
            //Assert
            Assert.Throws<EntityNotFoundException>(() => useCase.Execute(1));
        }
    }
}
