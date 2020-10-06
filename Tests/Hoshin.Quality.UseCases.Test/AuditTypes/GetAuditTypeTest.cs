using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllAuditTypesUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetOneAuditTypeUseCase;
using Hoshin.Quality.Domain.AuditType;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hoshin.Quality.UseCases.Test.AuditTypes
{
    public class GetAuditTypeTest
    {
        [Fact]
        public async void GetAllAuditTypesReturnsThreeTest()
        {
            //Arrange
            List<AuditType> list = new List<AuditType>();
            AuditTypeOutput atO1 = new AuditTypeOutput() { Id = 1, Code = "Codigo 1", Name = "Nombre 1", Active = true };
            AuditTypeOutput atO2 = new AuditTypeOutput() { Id = 2, Code = "Codigo 2", Name = "Nombre 2", Active = true };
            AuditTypeOutput atO3 = new AuditTypeOutput() { Id = 3, Code = "Codigo 3", Name = "Nombre 3", Active = false };
            List<AuditTypeOutput> listO = new List<AuditTypeOutput>() { atO1, atO2, atO3 };

            var mockAuditTypeRepository = new Mock<IAuditTypeRepository>();
            var mockMapper = new Mock<IMapper>();

            mockAuditTypeRepository.Setup(e => e.GetAll()).ReturnsAsync(list);
            mockMapper.Setup(e => e.Map<List<AuditType>, List<AuditTypeOutput>>(It.IsAny<List<AuditType>>())).Returns(listO);

            var useCase = new GetAllAuditTypesUseCase(mockAuditTypeRepository.Object, mockMapper.Object);

            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.Equal(3, res.Count);
        }

        [Fact]
        public async void GetAllActiveAuditTypesReturnsTwoTest()
        {
            //Arrange
            List<AuditType> list = new List<AuditType>();
            AuditTypeOutput atO1 = new AuditTypeOutput() { Id = 1, Code = "Codigo 1", Name = "Nombre 1", Active = true };
            AuditTypeOutput atO2 = new AuditTypeOutput() { Id = 2, Code = "Codigo 2", Name = "Nombre 2", Active = true };
            List<AuditTypeOutput> listO = new List<AuditTypeOutput>() { atO1, atO2 };

            var mockAuditTypeRepository = new Mock<IAuditTypeRepository>();
            var mockMapper = new Mock<IMapper>();

            mockAuditTypeRepository.Setup(e => e.GetAllActives()).ReturnsAsync(list);
            mockMapper.Setup(e => e.Map<List<AuditType>, List<AuditTypeOutput>>(It.IsAny<List<AuditType>>())).Returns(listO);

            var useCase = new GetAllAuditTypesUseCase(mockAuditTypeRepository.Object, mockMapper.Object);

            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.Equal(2, res.Count);
        }

        [Fact]
        public void GetOneAuditTypeReturnsSuccessfullyTest()
        {
            //Arrange
            AuditType at = new AuditType("Codigo", "Nombre", true, 1);
            AuditTypeOutput atO = new AuditTypeOutput() { Id = 1, Code = "Codigo", Name = "Nombre", Active = true };

            var mockAuditTypeRepository = new Mock<IAuditTypeRepository>();
            var mockMapper = new Mock<IMapper>();

            mockAuditTypeRepository.Setup(e => e.Get(It.IsAny<int>())).Returns(at);
            mockMapper.Setup(e => e.Map<AuditType, AuditTypeOutput>(It.IsAny<AuditType>())).Returns(atO);

            var useCase = new GetOneAuditTypeUseCase(mockAuditTypeRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<AuditTypeOutput>(res);
        }

        [Fact]
        public void GetOneAuditTypeReturnsNullTest()
        {
            //Arrange
            var mockAuditTypeRepository = new Mock<IAuditTypeRepository>();
            var mockMapper = new Mock<IMapper>();

            mockAuditTypeRepository.Setup(e => e.Get(It.IsAny<int>())).Returns<AuditType>(null);
            mockMapper.Setup(e => e.Map<AuditType, AuditTypeOutput>(It.IsAny<AuditType>())).Returns<AuditTypeOutput>(null);

            var useCase = new GetOneAuditTypeUseCase(mockAuditTypeRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.Null(res);
        }
    }
}
