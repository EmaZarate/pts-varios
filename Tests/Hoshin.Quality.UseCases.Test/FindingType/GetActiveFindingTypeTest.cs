using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetAllActiveFindingType;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.FindingType
{
    public class GetActiveFindingTypeTest
    {
        [Fact]
        public void ExecuteGetAllActiveFindingTypeTestReturnsOneOrMore()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();

            FindingTypeOutput f1 = new FindingTypeOutput { Id = 1, Name = "Test1", Code = "TE1", Active = true };
            FindingTypeOutput f2 = new FindingTypeOutput { Id = 2, Name = "Test2", Code = "TE2", Active = true };
            FindingTypeOutput f3 = new FindingTypeOutput { Id = 3, Name = "Test3", Code = "TE3", Active = true };

            mockMapper.Setup(e => e.Map<List<Domain.FindingType.FindingType>, List<FindingTypeOutput>>(It.IsAny<List<Domain.FindingType.FindingType>>())).Returns(new List<FindingTypeOutput> { f1, f2, f3 });
            mockFindingTypeRepository.Setup(e => e.GetAllActive()).Returns(new List<Domain.FindingType.FindingType>());

            var getAllActiveFindingType = new GetAllActiveFindingTypeUseCase(mockFindingTypeRepository.Object, mockMapper.Object);

            //Act
            var findingTypes = getAllActiveFindingType.Execute();

            //Assert
            Assert.NotEmpty(findingTypes);
        }

        [Fact]
        public void ExecuteGetAllActiveFindingTypeTestReturnsEmptyList()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();

            mockMapper.Setup(e => e.Map<List<Domain.FindingType.FindingType>, List<FindingTypeOutput>>(It.IsAny<List<Domain.FindingType.FindingType>>())).Returns(new List<FindingTypeOutput>());
            mockFindingTypeRepository.Setup(e => e.GetAllActive()).Returns(new List<Domain.FindingType.FindingType>());

            var getAllActiveFindingType = new GetAllActiveFindingTypeUseCase(mockFindingTypeRepository.Object, mockMapper.Object);

            //Act
            var findingTypes = getAllActiveFindingType.Execute();

            //Assert
            Assert.Empty(findingTypes);
        }
    }
}
