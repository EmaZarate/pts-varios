using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Core.Application.UseCases.AssignJobsSectorsPlant;
using Hoshin.Core.Domain.Plant;
using Hoshin.CrossCutting.Authorization.Claims.Core;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plant = Hoshin.Core.Domain.Plant.Plant;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.AssignJobsSectorsPlant
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class AssignJobsSectorsPlantController : ControllerBase
    {
        private readonly IAssignJobsSectorsPlantUseCase _assignJobsSectorsPlantUseCase;
        private readonly IMapper _mapper;
        public AssignJobsSectorsPlantController(IAssignJobsSectorsPlantUseCase assignJobsSectorsPlantUseCase, IMapper mapper)
        {
            _assignJobsSectorsPlantUseCase = assignJobsSectorsPlantUseCase;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(Policy = Administration.ConfigurePlants)]
        public IActionResult UpdatePlant([FromBody] AssignJobsSectorsPlantsDTO assignJobsSectorsPlantsDTO)
        {
            return new OkObjectResult(_assignJobsSectorsPlantUseCase.Execute(_mapper.Map<AssignJobsSectorsPlantsDTO, Plant>(assignJobsSectorsPlantsDTO)));
        }
    }
}