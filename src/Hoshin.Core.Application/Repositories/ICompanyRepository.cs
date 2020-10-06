using Hoshin.Core.Domain.Company;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAll();
        Company GetOne(int id);
        Company Add(Company company);
        Company Update(Company company);
    }
}
