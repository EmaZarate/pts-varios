using Hoshin.Core.Domain.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDCompany.UpdateCompanyUseCase
{
    public interface IUpdateCompanyUseCase
    {
        CompanyOutput Execute(Company company);
    }
}
