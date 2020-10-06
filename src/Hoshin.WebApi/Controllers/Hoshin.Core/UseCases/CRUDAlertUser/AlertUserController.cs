using AutoMapper;
using Hoshin.Core.Application.UseCases.AlertUser.GetAllAlertUser;
using Hoshin.Core.Application.UseCases.AlertUser.UpdateAlertUserUseCase;
using Hoshin.Core.Domain.AlertUser;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDAlertUser
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class AlertUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAllAlertUserUseCase _getAllAlertUserUseCase;
        private readonly IUpdateAlertUserUseCase _updateAlertUserUseCase;


        public AlertUserController(
            IMapper mapper,
            IGetAllAlertUserUseCase getAllAlertUserUseCase,
             IUpdateAlertUserUseCase updateAlertUserUseCase)
        {
            _mapper = mapper;
            _getAllAlertUserUseCase = getAllAlertUserUseCase;
            _updateAlertUserUseCase = updateAlertUserUseCase;

        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            return new OkObjectResult(await _getAllAlertUserUseCase.Execute(userId));
        }


        [HttpPost]
        public IActionResult InsertOrUpdate([FromBody] object alertUser)
        {            
            Dictionary<string, List<AlertUser>> dicDomain = _mapper.Map<Dictionary<string, List<AlertUserDTO>>, Dictionary<string, List<AlertUser>>>(JsonConvert.DeserializeObject<Dictionary<string, List<AlertUserDTO>>>(alertUser.ToString()));
            return new OkObjectResult(_updateAlertUserUseCase.Execute(dicDomain));
        }

    }

}
