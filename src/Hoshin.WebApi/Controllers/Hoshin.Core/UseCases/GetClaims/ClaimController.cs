using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.Core.Application.UseCases.Claim;
using Hoshin.CrossCutting.Authorization.Claims;
using Hoshin.CrossCutting.Authorization.Claims.Core;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.GetClaims
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class ClaimController : ControllerBase
    {
        private readonly IGetAllClaimsUseCase _getAllClaimsUseCase;
        public ClaimController(IGetAllClaimsUseCase getAllClaimsUseCase)
        {
            _getAllClaimsUseCase = getAllClaimsUseCase;
        }
        [HttpGet]
        [Authorize(Policy = Claims.ViewClaims)]
        [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllClaimsUseCase.Execute());
        }
    }
}