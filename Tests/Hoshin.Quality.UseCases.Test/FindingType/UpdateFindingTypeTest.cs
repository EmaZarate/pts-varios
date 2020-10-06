using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FindingType;
using Hoshin.Quality.Application.UseCases.FindingType.UpdateFindingType;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.FindingType
{
    public class UpdateFindingTypeTest
    {
        [Fact]
        public void ExecuteWithExistFindingTypeTest()
        {
            //Arrange
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();
            var mockMapper = new Mock<IMapper>();

            mockFindingTypeRepository.Setup(e => e.Get(It.IsAny<int>())).Returns(new Domain.FindingType.FindingType());
            mockFindingTypeRepository.Setup(e => e.Update(It.IsAny<Domain.FindingType.FindingType>())).Returns(new Domain.FindingType.FindingType());
            mockMapper.Setup(e => e.Map<Domain.FindingType.FindingType, FindingTypeOutput>(It.IsAny<Domain.FindingType.FindingType>())).Returns(new FindingTypeOutput(1,"name", "code", true, new List<Domain.FindingType.FindingTypeParametrization>()));
            var findingtype = new Domain.FindingType.FindingType("name", "code", true, new List<Domain.FindingType.FindingTypeParametrization>());
            var useCase = new UpdateFindingTypeUseCase(mockFindingTypeRepository.Object, mockMapper.Object);
            //Act
            var res = useCase.Execute(findingtype);

            //Assert
            Assert.IsType<FindingTypeOutput>(res);
        }
    }
}
