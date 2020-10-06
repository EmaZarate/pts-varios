using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDPlant;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetAllPlantsUseCase;
using Hoshin.Core.Application.UseCases.CRUDSector;
using Hoshin.Core.Application.UseCases.CRUDSector.GetAllSectorsUseCase;
using Hoshin.Core.Domain.Plant;
using Hoshin.Core.Domain.Sector;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Sectors
{
    public class GetAllSectorsTest
    {
        [Fact]
        public async void GetAllSectorsReturnEmptyListTest()
        {
            //Arrange
            List<Sector> list = new List<Sector>();

            var mockSectorRepository = new Mock<ISectorRepository>();
            var mockMapper = new Mock<IMapper>();

            mockSectorRepository.Setup(e => e.GetAll()).ReturnsAsync(list);
            mockMapper.Setup(e => e.Map<List<Sector>, List<SectorOutput>>(It.IsAny<List<Sector>>())).Returns(new List<SectorOutput>());

            var useCase = new GetAllSectorsUseCase(mockSectorRepository.Object, mockMapper.Object);

            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.Empty(res);
        }

        [Fact]
        public async void GetAllSectorsReturnListWith3SectorsTest()
        {
            //Arrange
            List<Sector> list = new List<Sector>();
            SectorOutput s1 = new SectorOutput { SectorId = 1, Name = "Nombre 1", Code = "Código 1", Description = "Descripción 1", Active = true };
            SectorOutput s2 = new SectorOutput { SectorId = 2, Name = "Nombre 2", Code = "Código 2", Description = "Descripción 2", Active = true };
            SectorOutput s3 = new SectorOutput { SectorId = 3, Name = "Nombre 3", Code = "Código 3", Description = "Descripción 3", Active = false };

            var mockSectorRepository = new Mock<ISectorRepository>();
            var mockMapper = new Mock<IMapper>();

            mockSectorRepository.Setup(e => e.GetAll()).ReturnsAsync(list);
            mockMapper.Setup(e => e.Map<List<Sector>, List<SectorOutput>>(It.IsAny<List<Sector>>())).Returns(new List<SectorOutput> { s1, s2, s3 });

            var useCase = new GetAllSectorsUseCase(mockSectorRepository.Object, mockMapper.Object);

            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.Equal(3, res.Count);
        }
    }
}
