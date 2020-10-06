using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDPlant;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetAllPlantsUseCase;
using Hoshin.Core.Domain.Plant;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Plants
{
    public class GetAllPlantsTest
    {
        [Fact]
        public async void GetAllPlantsReturnEmptyListTest()
        {
            //Arrange
            List<Plant> list = new List<Plant>();

            var mockPlantRepository = new Mock<IPlantRepository>();
            var mockMapper = new Mock<IMapper>();

            mockPlantRepository.Setup(e => e.GetAll()).ReturnsAsync(list);
            mockMapper.Setup(e => e.Map<List<Plant>, List<PlantOutput>>(It.IsAny<List<Plant>>())).Returns(new List<PlantOutput>());

            var useCase = new GetAllPlantsUseCase(mockPlantRepository.Object, mockMapper.Object);

            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.Empty(res);
        }

        [Fact]
        public async void GetAllPlantsReturnListWith3PlantsTest()
        {
            //Arrange
            List<Plant> list = new List<Plant>();
            PlantOutput p1 = new PlantOutput { PlantID = 1, Name = "Nombre 1", Country = "País 1", Active = true };
            PlantOutput p2 = new PlantOutput { PlantID = 2, Name = "Nombre 2", Country = "País 2", Active = true };
            PlantOutput p3 = new PlantOutput { PlantID = 3, Name = "Nombre 3", Country = "País 3", Active = false };

            var mockPlantRepository = new Mock<IPlantRepository>();
            var mockMapper = new Mock<IMapper>();

            mockPlantRepository.Setup(e => e.GetAll()).ReturnsAsync(list);
            mockMapper.Setup(e => e.Map<List<Plant>, List<PlantOutput>>(It.IsAny<List<Plant>>())).Returns(new List<PlantOutput> { p1, p2, p3 });

            var useCase = new GetAllPlantsUseCase(mockPlantRepository.Object, mockMapper.Object);

            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.Equal(3, res.Count);            
        }
    }
}
