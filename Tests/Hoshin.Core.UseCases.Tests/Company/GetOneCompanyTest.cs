using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDCompany;
using Hoshin.Core.Application.UseCases.CRUDCompany.GetOneCompanyUseCase;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Company
{
    public class GetOneCompanyTest
    {
        [Fact]
        public void GetOneCompanyReturnsSuccessfullyTest()
        {
            //Arrange
            Domain.Company.Company c = new Domain.Company.Company(1, "Company 1", "CUIT", "Dirección", "Teléfono", null);
            CompanyOutput cO = new CompanyOutput() { CompanyID = 1, BusinessName = "Company 1", CUIT = "CUIT", Address = "Dirección", PhoneNumber = "Teléfono", Logo = null };

            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var mockMapper = new Mock<IMapper>();

            mockCompanyRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns(c);
            mockMapper.Setup(e => e.Map<Domain.Company.Company, CompanyOutput>(It.IsAny<Domain.Company.Company>())).Returns(cO);

            var useCase = new GetOneCompanyUseCase(mockCompanyRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.IsType<CompanyOutput>(res);
        }

        [Fact]
        public void GetOneCompanyReturnsNullTest()
        {
            //Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var mockMapper = new Mock<IMapper>();

            mockCompanyRepository.Setup(e => e.GetOne(It.IsAny<int>())).Returns<CompanyOutput>(null);
            mockMapper.Setup(e => e.Map<Domain.Company.Company, CompanyOutput>(It.IsAny<Domain.Company.Company>())).Returns<CompanyOutput>(null);

            var useCase = new GetOneCompanyUseCase(mockCompanyRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(1);

            //Assert
            Assert.Null(res);
        }
    }
}
