using Hoshin.Core.Domain.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDCompany.AddCompanyUseCase
{
    public interface IAddCompanyUseCase
    {
        CompanyOutput Execute(Company company);
    }
}
