using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Domain.Company;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;

        public CompanyRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<List<Company>> GetAll()
        {
            var list = await _ctx.Companies.ToListAsync();

            return _mapper.Map<List<Companies>, List<Company>>(list);
        }

        public Company GetOne(int id)
        {
            return _mapper.Map<Companies, Company>(_ctx.Companies.Find(id));
        }

        public Company Add(Company company)
        {
            Companies companyDb = _mapper.Map<Company, Companies>(company);
            _ctx.Companies.Add(companyDb);
            _ctx.SaveChanges();

            return _mapper.Map<Companies, Company>(companyDb);
        }

        public Company Update(Company company)
        {
            Companies companyDb = _mapper.Map<Company, Companies>(company);
            _ctx.Companies.Update(companyDb);
            _ctx.SaveChanges();

            return _mapper.Map<Companies, Company>(companyDb);
        }
    }
}
