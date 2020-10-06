using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDPlant;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetOnePlantUseCase;
using Hoshin.Core.Domain.Plant;
using Moq;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Plants
{
    public class GetOnePlantTest
    {
        [Fact]
        public async void GetOnePlantReturnNullTest()
        {
            //Arrange
            Plant p = new Plant();

            var mockPlantRepository = new Mock<IPlantRepository>();
            var mockMapper = new Mock<IMapper>();

            mockPlantRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns<Plant>(null);
            mockMapper.Setup(e => e.Map<Plant, PlantOutput>(It.IsAny<Plant>())).Returns<PlantOutput>(null);

            var useCase = new GetOnePlantUseCase(mockPlantRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetOnePlantReturn1PlantTest()
        {
            //Arrange
            Plant p = new Plant(1, "Nombre 1", "País 1", true);
            PlantOutput p1 = new PlantOutput { PlantID = 1, Name = "Nombre 1", Country = "País 1", Active = true };

            var mockPlantRepository = new Mock<IPlantRepository>();
            var mockMapper = new Mock<IMapper>();

            mockPlantRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns(p);
            mockMapper.Setup(e => e.Map<Plant, PlantOutput>(It.IsAny<Plant>())).Returns(p1);

            var useCase = new GetOnePlantUseCase(mockPlantRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<PlantOutput>(res);
        }
    }
}
