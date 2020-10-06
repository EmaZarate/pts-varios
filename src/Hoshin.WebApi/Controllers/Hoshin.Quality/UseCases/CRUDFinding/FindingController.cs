using Hoshin.Quality.Application.UseCases.Finding.GetAllFinding;
using Hoshin.Quality.Application.UseCases.Finding.GetCountFinding;
using Hoshin.Quality.Application.UseCases.Finding.GetOneFinding;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Hoshin.Quality.Application.UseCases.Finding;
using System.Collections.Generic;
using Hoshin.Quality.Application.UseCases.Finding.UpdateFinding;
using AutoMapper;
using Hoshin.Quality.Domain.Finding;
using Hoshin.Quality.Application.UseCases.Finding.GetAllApprovedInProgressFinding;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFinding
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class FindingController : ControllerBase
    {
        private readonly IGetAllFindingUseCase _getAllFindingUseCase;
        private readonly IGetAllApprovedInProgressFindingUseCase _getAllApprovedInProgressFindingUseCase;
        private readonly IGetCountFindingUseCase _getCountFindingUseCase;
        private readonly IGetOneFindingUseCase _getOneFindingUseCase;
        private readonly IUpdateFindingUseCase _updateFindingUseCase;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public FindingController(
            IGetAllFindingUseCase getAllFindingUseCase,
            IGetAllApprovedInProgressFindingUseCase getAllApprovedInProgressFindingUseCase,
            IGetCountFindingUseCase getCountFindingUseCase,
            IGetOneFindingUseCase getOneFindingUseCase,
            IUpdateFindingUseCase updateFindingUseCase,
            IMapper mapper,
            IDistributedCache cache
            )
        {
            _getAllFindingUseCase = getAllFindingUseCase;
            _getAllApprovedInProgressFindingUseCase = getAllApprovedInProgressFindingUseCase;
            _getCountFindingUseCase = getCountFindingUseCase;
            _getOneFindingUseCase = getOneFindingUseCase;
            _updateFindingUseCase = updateFindingUseCase;
            _mapper = mapper;
            _cache = cache;
        }

        /// <summary>
        /// Get finding by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = Findings.ReadSector)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneFindingUseCase.Execute(id));
        }

        /// <summary>
        /// Get all findings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Findings.ReadSector)]
      //  [ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> Get()
        {
             return new OkObjectResult(await _getAllFindingUseCase.Execute());
        }

        /// <summary>
        /// Get all findings approved and in progress
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetApprovedInProgress()
        {
            return new OkObjectResult(_getAllApprovedInProgressFindingUseCase.Execute());
        }

        /// <summary>
        /// Get count of findings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Findings.ReadSector)]
        public IActionResult GetCount()
        {
            return new OkObjectResult(_getCountFindingUseCase.Execute());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles([FromForm] IFormCollection test)
        {
            using (var sr = new StreamReader(test.Files[0].OpenReadStream()))
            {
                var body = await sr.ReadToEndAsync();
                //return Ok(body);
            }
            var req = test;
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] FindingDTO finding)
        {
            return new OkObjectResult(_updateFindingUseCase.Execute(_mapper.Map<FindingDTO, Finding>(finding)));
        }


    }


    
}