using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDCompany;
using Hoshin.Core.Application.UseCases.CRUDCompany.AddCompanyUseCase;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Company
{
    public class AddCompanyTest
    {
        [Fact]
        public void AddCompanyReturnCreatedCompanyTest()
        {
            //Arrange
            Domain.Company.Company cm = new Domain.Company.Company();
            Domain.Company.Company c = new Domain.Company.Company(1, "Razón Social 1", "CUIT", "Dirección", "Teléfono", null);
            CompanyOutput co = new CompanyOutput() { CompanyID = 1, BusinessName = "Razón Social 1", Address = "Dirección", PhoneNumber = "Teléfono", Logo = null };
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var mockMapper = new Mock<IMapper>();

            mockCompanyRepository.Setup(e => e.Add(It.IsAny<Domain.Company.Company>())).Returns(c);
            mockMapper.Setup(e => e.Map<Domain.Company.Company, CompanyOutput>(It.IsAny<Domain.Company.Company>())).Returns(co);

            var useCase = new AddCompanyUseCase(mockCompanyRepository.Object, mockMapper.Object);

            //Act
            var res = useCase.Execute(cm);

            //Assert
            Assert.IsType<CompanyOutput>(res);
        }
    }
}
