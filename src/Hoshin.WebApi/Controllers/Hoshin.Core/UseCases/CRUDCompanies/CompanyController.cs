using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Core.Application.UseCases.CRUDCompany.AddCompanyUseCase;
using Hoshin.Core.Application.UseCases.CRUDCompany.GetAllCompaniesUseCase;
using Hoshin.Core.Application.UseCases.CRUDCompany.GetOneCompanyUseCase;
using Hoshin.Core.Application.UseCases.CRUDCompany.UpdateCompanyUseCase;
using Hoshin.Core.Domain.Company;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyClaim = Hoshin.CrossCutting.Authorization.Claims.Core.Company;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDCompanies
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class CompanyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAllCompaniesUseCase _getAllCompaniesUseCase;
        private readonly IGetOneCompanyUseCase _getOneCompanyUseCase;
        private readonly IAddCompanyUseCase _addCompanyUseCase;
        private readonly IUpdateCompanyUseCase _updateCompanyUseCase;

        public CompanyController(
            IMapper mapper,
            IGetAllCompaniesUseCase getAllCompaniesUseCase,
            IGetOneCompanyUseCase getOneCompanyUseCase,
            IAddCompanyUseCase addCompanyUseCase,
            IUpdateCompanyUseCase updateCompanyUseCase
            )
        {
            _mapper = mapper;
            _getAllCompaniesUseCase = getAllCompaniesUseCase;
            _getOneCompanyUseCase = getOneCompanyUseCase;
            _addCompanyUseCase = addCompanyUseCase;
            _updateCompanyUseCase = updateCompanyUseCase;
        }

        [HttpGet]
        [Authorize(Policy = CompanyClaim.ReadCompany)]
        [ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _getAllCompaniesUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = CompanyClaim.ReadCompany)]
        public IActionResult GetOne(int id)
        {
            return new OkObjectResult(_getOneCompanyUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = CompanyClaim.AddCompany)]
        public IActionResult Add([FromBody] CompanyDTO company)
        {
            return new OkObjectResult(_addCompanyUseCase.Execute(_mapper.Map<CompanyDTO, Company>(company)));
        }

        [HttpPut]
        [Authorize(Policy = CompanyClaim.EditCompany)]
        public IActionResult Update([FromBody] CompanyDTO company)
        {
            return new OkObjectResult(_updateCompanyUseCase.Execute(_mapper.Map<CompanyDTO, Company>(company)));
        }
    }
}