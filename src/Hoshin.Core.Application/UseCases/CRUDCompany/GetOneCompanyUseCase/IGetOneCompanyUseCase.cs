using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDCompany.GetOneCompanyUseCase
{
    public interface IGetOneCompanyUseCase
    {
        CompanyOutput Execute(int id);
    }
}
