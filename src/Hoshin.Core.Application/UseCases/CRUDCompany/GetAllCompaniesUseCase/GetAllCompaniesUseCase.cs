using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Company;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDCompany.GetAllCompaniesUseCase
{
    public class GetAllCompaniesUseCase : IGetAllCompaniesUseCase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetAllCompaniesUseCase(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<List<CompanyOutput>> Execute()
        {
            var list = await _companyRepository.GetAll();
            return _mapper.Map<List<Company>, List<CompanyOutput>>(list);
        }
    }
}
