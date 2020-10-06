using AutoMapper;
using Hoshin.Core.Application.Exceptions.Plant;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDPlant;
using Hoshin.Core.Application.UseCases.CRUDPlant.AddPlantUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetAllPlantsUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetOnePlantUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.UpdatePlantUseCase;
using Hoshin.Core.Domain.Plant;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Plants
{
    public class UpdatePlantTest
    {
        [Fact]
        public async void UpdatePlantReturnUpdatedPlantTest()
        {
            //Arrange
            Plant pe = new Plant();
            Plant p = new Plant(1, "Nombre 1", "País 1", true);
            PlantOutput po = new PlantOutput() { PlantID = 1, Name = "Nombre 1", Country = "País 1", Active = true };
            var mockPlantRepository = new Mock<IPlantRepository>();
            var mockMapper = new Mock<IMapper>();

            mockPlantRepository.Setup(e => e.Update(It.IsAny<Plant>())).Returns(p);
            mockMapper.Setup(e => e.Map<Plant, PlantOutput>(It.IsAny<Plant>())).Returns(po);

            var useCase = new UpdatePlantUseCase(mockPlantRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(pe);

            //Assert
            Assert.IsType<PlantOutput>(res);
        }

        [Fact]
        public async void UpdatePlantThrowPlantWithThisNameAndCountryAlreadyExistsExceptionTest()
        {
            //Arrange
            Plant p = new Plant(1, "Nombre 1", "País 1", true);

            var mockPlantRepository = new Mock<IPlantRepository>();
            var mockMapper = new Mock<IMapper>();

            mockPlantRepository.Setup(e => e.CheckDuplicated(It.IsAny<Plant>())).Returns(p);

            var useCase = new UpdatePlantUseCase(mockPlantRepository.Object, mockMapper.Object);

            //Act

            //Assert
            Assert.Throws<PlantWithThisNameAndCountryAlreadyExists>(() => useCase.Execute(p));
        }
    }
}

