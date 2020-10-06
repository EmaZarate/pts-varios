using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Core.Application.UseCases.CRUDJob.AddJobUseCase;
using Hoshin.Core.Application.UseCases.CRUDJob.GetAllJobsUseCase;
using Hoshin.Core.Application.UseCases.CRUDJob.GetOneJobUseCase;
using Hoshin.Core.Application.UseCases.CRUDJob.UpdateJobUseCase;
using Hoshin.Core.Domain.Job;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobClaim = Hoshin.CrossCutting.Authorization.Claims.Core.Job;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDJobs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class JobController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAllJobsUseCase _getAllJobsUseCase;
        private readonly IGetOneJobUseCase _getOneJobUseCase;
        private readonly IAddJobUseCase _addJobUseCase;
        private readonly IUpdateJobUseCase _updateJobUseCase;

        public JobController(
            IMapper mapper,
            IGetAllJobsUseCase getAllJobsUseCase,
            IGetOneJobUseCase getOneJobUseCase,
            IAddJobUseCase addJobUseCase,
            IUpdateJobUseCase updateJobUseCase
            )
        {
            _mapper = mapper;
            _getAllJobsUseCase = getAllJobsUseCase;
            _getOneJobUseCase = getOneJobUseCase;
            _addJobUseCase = addJobUseCase;
            _updateJobUseCase = updateJobUseCase;
        }

        [HttpGet]
        [Authorize(JobClaim.ViewJob)]
       // [ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _getAllJobsUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(JobClaim.ViewJob)]
        public IActionResult GetOne(int id)
        {
            return new OkObjectResult(_getOneJobUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(JobClaim.AddJob)]
        public IActionResult Add([FromBody] JobDTO job)
        {
            return new OkObjectResult(_addJobUseCase.Execute(_mapper.Map<JobDTO, Job>(job)));
        }

        [HttpPut]
        [Authorize(JobClaim.EditJob)]
        [Authorize(JobClaim.DeactivateJob)]
        [Authorize(JobClaim.ActivateJob)]
        public IActionResult Update([FromBody] JobDTO job)
        {
            return new OkObjectResult(_updateJobUseCase.Execute(_mapper.Map<JobDTO, Job>(job)));
        }
    }
}