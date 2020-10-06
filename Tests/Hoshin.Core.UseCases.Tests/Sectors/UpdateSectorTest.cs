using AutoMapper;
using Hoshin.Core.Application.Exceptions.Sector;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDSector;
using Hoshin.Core.Application.UseCases.CRUDSector.UpdateSectorUseCase;
using Hoshin.Core.Domain.Sector;
using Moq;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Sectors
{
    public class UpdateSectorTest
    {
        [Fact]
        public async void UpdateSectorReturnUpdatedSectorTest()
        {
            //Arrange
            Sector se = new Sector();
            Sector s = new Sector(1, "Nombre 1", "Código 1", "Descripción 1", true);
            SectorOutput so = new SectorOutput() { SectorId = 1, Name = "Nombre 1", Code = "Código 1", Description = "Descripción 1", Active = true };
            var mockSectorRepository = new Mock<ISectorRepository>();
            var mockMapper = new Mock<IMapper>();

            mockSectorRepository.Setup(e => e.Update(It.IsAny<Sector>())).Returns(s);
            mockMapper.Setup(e => e.Map<Sector, SectorOutput>(It.IsAny<Sector>())).Returns(so);

            var useCase = new UpdateSectorUseCase(mockSectorRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(se);

            //Assert
            Assert.IsType<SectorOutput>(res);
        }

        [Fact]
        public async void UpdateSectorThrowSectorWithThisNameAndOrCodeAlreadyExistsExceptionTest()
        {
            //Arrange
            Sector s = new Sector(1, "Nombre 1", "Código 1", "Descripción 1", true);

            var mockSectorRepository = new Mock<ISectorRepository>();
            var mockMapper = new Mock<IMapper>();

            mockSectorRepository.Setup(e => e.CheckDuplicated(It.IsAny<Sector>())).Returns(s);

            var useCase = new UpdateSectorUseCase(mockSectorRepository.Object, mockMapper.Object);

            //Act

            //Assert
            Assert.Throws<SectorWithThisNameAndOrCodeAlreadyExists>(() => useCase.Execute(s));
        }
    }
}

