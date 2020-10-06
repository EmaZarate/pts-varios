using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetAllFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetOneFindingType;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.FindingType
{
    public class GetFindingTypesTest
    {
        [Fact]
        public void ExecuteGetAllFindingTypeTest()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();

            mockMapper.Setup(e => e.Map<List<Domain.FindingType.FindingType>, List<FindingTypeOutput>>(It.IsAny<List<Domain.FindingType.FindingType>>())).Returns(new List<FindingTypeOutput>());
            mockFindingTypeRepository.Setup(e => e.GetAll()).Returns(new List<Domain.FindingType.FindingType>());

            var useCase = new GetAllFindingTypeUseCase(mockFindingTypeRepository.Object, mockMapper.Object);
            
            //Act
            var res = useCase.Execute();
            
            //Assert

            Assert.IsType<List<FindingTypeOutput>>(res);
        }

        [Fact]
        public void ExecuteGetOneExistFindingTypeTest()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();

            mockMapper.Setup(e => 
                e.Map<Domain.FindingType.FindingType, FindingTypeOutput>(It.IsAny<Domain.FindingType.FindingType>()))
                .Returns(new FindingTypeOutput(1, "name","code",true,new List<Domain.FindingType.FindingTypeParametrization>()));
            mockFindingTypeRepository.Setup(e =>
                e.Get(It.IsAny<int>()))
                .Returns(new Domain.FindingType.FindingType());

            var useCase = new GetOneFindingTypeUseCase(mockFindingTypeRepository.Object, mockMapper.Object);
            //Act

            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<FindingTypeOutput>(res);
        }

        [Fact]
        public void ExecuteGetOneWithNoExistFindingTypeTest()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockFindingTypeRepository = new Mock<IFindingTypeRepository>();

            mockMapper.Setup(e =>
                e.Map<Domain.FindingType.FindingType, FindingTypeOutput>(It.IsAny<Domain.FindingType.FindingType>()))
                .Returns(new FindingTypeOutput(1, "name", "code", true, new List<Domain.FindingType.FindingTypeParametrization>()));
            mockFindingTypeRepository.Setup(e =>
                e.Get(It.IsAny<int>()))
                .Returns<Domain.FindingType.FindingType>(null);

            var useCase = new GetOneFindingTypeUseCase(mockFindingTypeRepository.Object, mockMapper.Object);

            //Act
            //Assert
            Assert.Throws<EntityNotFoundException>(() => useCase.Execute(1));
        }
    }
}
