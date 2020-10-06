using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDSector;
using Hoshin.Core.Application.UseCases.CRUDSector.GetOneSectorUseCase;
using Hoshin.Core.Domain.Sector;
using Moq;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Sectors
{
    public class GetOneSectorTest
    {
        [Fact]
        public async void GetOneSectorReturnNullTest()
        {
            //Arrange
            Sector s = new Sector();

            var mockSectorRepository = new Mock<ISectorRepository>();
            var mockMapper = new Mock<IMapper>();

            mockSectorRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns<Sector>(null);
            mockMapper.Setup(e => e.Map<Sector, SectorOutput>(It.IsAny<Sector>())).Returns<SectorOutput>(null);

            var useCase = new GetOneSectorUseCase(mockSectorRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetOneSectorReturn1SectorTest()
        {
            //Arrange
            Sector s = new Sector(1, "Nombre 1", "Código 1", "Descripción 1", true);
            SectorOutput s1 = new SectorOutput { SectorId = 1, Name = "Nombre 1", Code = "Código 1", Description = "Descripción 1", Active = true };

            var mockSectorRepository = new Mock<ISectorRepository>();
            var mockMapper = new Mock<IMapper>();

            mockSectorRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns(s);
            mockMapper.Setup(e => e.Map<Sector, SectorOutput>(It.IsAny<Sector>())).Returns(s1);

            var useCase = new GetOneSectorUseCase(mockSectorRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<SectorOutput>(res);
        }
    }
}
