using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.Standard.CreateStandard;
using Hoshin.Quality.Application.UseCases.Standard.GetAllStandard;
using Hoshin.Quality.Application.UseCases.Standard.GetOneStandard;
using Hoshin.Quality.Application.UseCases.Standard.UpdateStandard;
using Hoshin.Quality.Domain.Standard;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StandardsClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.Standards;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDStandard
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class StandardController : ControllerBase
    {

        private readonly IGetAllStandardUseCase _getAllStandardUseCase;
        private readonly IGetOneStandardUseCase _getOneStandardUseCase;
        private readonly ICreateStandardUseCase _createStandardUseCase;
        private readonly IUpdateStandardUseCase _updateStandardUseCase;
        private readonly IMapper _mapper;

        public StandardController(
                IGetAllStandardUseCase getAllStandardUseCase,
                IGetOneStandardUseCase getOneStandardUseCase,
                ICreateStandardUseCase createStandardUseCase,
                IUpdateStandardUseCase updateStandardUseCase,
                IMapper mapper
            )
        {
            _getAllStandardUseCase = getAllStandardUseCase;
            _getOneStandardUseCase = getOneStandardUseCase;
            _createStandardUseCase = createStandardUseCase;
            _updateStandardUseCase = updateStandardUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = StandardsClaim.ReadStandrads)]
      //  [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllStandardUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = StandardsClaim.ReadStandrads)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneStandardUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = StandardsClaim.AddStandards)]
        public IActionResult Add([FromBody] StandardDTO model)
        {
            _createStandardUseCase.Execute(_mapper.Map<StandardDTO, Standard>(model));
            return new OkResult();
        }

        [HttpPut]
        [Authorize(Policy = StandardsClaim.EditStandrads)]
        public IActionResult Update([FromBody] StandardDTO model)
        {
            _updateStandardUseCase.Execute(_mapper.Map<StandardDTO, Standard>(model));
            return new OkResult();
        }

    }

}
