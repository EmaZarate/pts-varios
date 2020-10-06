using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDCompany.GetAllCompaniesUseCase
{
    public interface IGetAllCompaniesUseCase
    {
        Task<List<CompanyOutput>> Execute();
    }
}
