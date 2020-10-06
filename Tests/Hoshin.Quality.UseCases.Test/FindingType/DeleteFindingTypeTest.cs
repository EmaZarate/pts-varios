using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FindingType.DeleteFindingType;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.FindingType
{
    public class DeleteFindingTypeTest
    {
        [Fact]
        public void ExecuteWithExistFindingTypeTest()
        {
            //Arrange
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();
            mockFindingTypeRepository.Setup(e => e.Delete(It.IsAny<int>())).Returns(true);

            var useCase = new DeleteFindingTypeUseCase(mockFindingTypeRepository.Object);
            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.True(res);
        }
    }
}
