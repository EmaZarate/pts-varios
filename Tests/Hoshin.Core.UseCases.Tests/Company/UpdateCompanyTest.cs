using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDCompany;
using Hoshin.Core.Application.UseCases.CRUDCompany.UpdateCompanyUseCase;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Company
{
    public class UpdateCompanyTest
    {
        [Fact]
        public void UpdateCompanyReturnsUpdatedCompanyTest()
        {
            //Arrange
            Domain.Company.Company cR = new Domain.Company.Company();
            Domain.Company.Company c = new Domain.Company.Company(1, "Company 1", "CUIT", "Dirección", "Phone Number", null);
            CompanyOutput cO = new CompanyOutput() { CompanyID = 1, BusinessName = "Company 1", CUIT = "CUIT", Address = "Dirección", PhoneNumber = "Phone Number", Logo = null };

            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var mockMapper = new Mock<IMapper>();

            mockCompanyRepository.Setup(e => e.Update(It.IsAny<Domain.Company.Company>())).Returns(c);
            mockMapper.Setup(e => e.Map<Domain.Company.Company, CompanyOutput>(It.IsAny<Domain.Company.Company>())).Returns(cO);

            var useCase = new UpdateCompanyUseCase(mockCompanyRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(cR);

            //Assert
            Assert.IsType<CompanyOutput>(res);
        }
    }
}
