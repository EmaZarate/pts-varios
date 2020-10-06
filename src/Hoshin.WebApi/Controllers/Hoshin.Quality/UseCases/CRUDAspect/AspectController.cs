using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.Aspect.GetOneAspectUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDAspect
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AspectController : ControllerBase
    {
        private readonly IGetOneAspectUseCase _getOneAspectUseCase;

        public AspectController(IGetOneAspectUseCase getOneAspectUseCase)
        {
            _getOneAspectUseCase = getOneAspectUseCase;
        }

        [Authorize(Policy = Aspects.ReadAspects)]
        [HttpGet("{standardId}/{aspectId}")]
        public IActionResult Get(int standardId, int aspectId)
        {
            return new OkObjectResult(_getOneAspectUseCase.Execute(standardId, aspectId));
        }
    }
}