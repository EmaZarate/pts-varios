using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDCompany.AddCompanyUseCase
{
    public class AddCompanyUseCase : IAddCompanyUseCase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public AddCompanyUseCase(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public CompanyOutput Execute(Company company)
        {
            return _mapper.Map<Company, CompanyOutput>(_companyRepository.Add(company));
        }
    }
}
