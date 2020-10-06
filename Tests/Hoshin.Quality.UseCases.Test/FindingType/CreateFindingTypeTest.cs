using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FindingType;
using Hoshin.Quality.Application.UseCases.FindingType.CreateFindingType;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.FindingType
{
    public class CreateFindingTypeTest
    {
        [Fact]
        public void ExecuteWithNewCreateFindingTypeTest()
        {
            //Arrange
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();
            mockFindingTypeRepository.Setup(x => x.Get(It.IsAny<string>())).Returns<Domain.FindingType.FindingType>(null);
            mockFindingTypeRepository.Setup(x => x.Add(It.IsAny<Domain.FindingType.FindingType>())).Returns(new Domain.FindingType.FindingType("name", "code", true, new List<Domain.FindingType.FindingTypeParametrization>()));

            var useCase = new CreateFindingTypeUseCase(mockFindingTypeRepository.Object);
            //Act
            var res = useCase.Execute("name","code",true, new List<Domain.FindingType.FindingTypeParametrization>());
            //Assert
            Assert.IsType<FindingTypeOutput>(res);
        }

        [Fact]
        public void ExecuteWithDuplicatedFindingTypeTest()
        {
            //Arrange
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();
            mockFindingTypeRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(new Domain.FindingType.FindingType("name", "code", true, new List<Domain.FindingType.FindingTypeParametrization>()));
            mockFindingTypeRepository.Setup(x => x.Add(It.IsAny<Domain.FindingType.FindingType>())).Returns(new Domain.FindingType.FindingType("name", "code", true, new List<Domain.FindingType.FindingTypeParametrization>()));

            var useCase = new CreateFindingTypeUseCase(mockFindingTypeRepository.Object);
            //Act
            //Assert
            Assert.Throws<DuplicateEntityException>(() => useCase.Execute("name", "code", true, new List<Domain.FindingType.FindingTypeParametrization>()));
            
            
        }
    }
}
