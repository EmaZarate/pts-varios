using AutoMapper;
using Hoshin.Core.Application.UseCases.Alert.GetAllAlert;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.Alert
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class AlertController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAllAlertUseCase _getAllAlertUseCase;


        public AlertController(
            IMapper mapper,
            IGetAllAlertUseCase getAllAlertUseCase)
        {
            _mapper = mapper;
            _getAllAlertUseCase = getAllAlertUseCase;

        }


        [HttpGet]
        [ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _getAllAlertUseCase.Execute());
        }


    }
}
