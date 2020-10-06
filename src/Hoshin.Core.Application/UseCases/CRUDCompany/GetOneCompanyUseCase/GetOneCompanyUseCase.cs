using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDCompany.GetOneCompanyUseCase
{
    public class GetOneCompanyUseCase : IGetOneCompanyUseCase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetOneCompanyUseCase(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public CompanyOutput Execute(int id)
        {
            return _mapper.Map<Company, CompanyOutput>(_companyRepository.GetOne(id));
        }
    }
}
