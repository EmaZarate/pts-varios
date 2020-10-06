using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Core.Application.UseCases.CRUDPlant.AddPlantUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetAllPlantsUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetOnePlantUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.UpdatePlantUseCase;
using Hoshin.Core.Domain.Plant;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantClaim = Hoshin.CrossCutting.Authorization.Claims.Core.Plant;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDPlants
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class PlantController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAllPlantsUseCase _getAllPlantsUseCase;
        private readonly IGetOnePlantUseCase _getOnePlantUseCase;
        private readonly IAddPlantUseCase _addPlantUseCase;
        private readonly IUpdatePlantUseCase _updatePlantUseCase;

        public PlantController(
            IMapper mapper,
            IGetAllPlantsUseCase getAllPlantsUseCase,
            IGetOnePlantUseCase getOnePlantUseCase,
            IAddPlantUseCase addPlantUseCase,
            IUpdatePlantUseCase updatePlantUseCase
            )
        {
            _mapper = mapper;
            _getAllPlantsUseCase = getAllPlantsUseCase;
            _getOnePlantUseCase = getOnePlantUseCase;
            _addPlantUseCase = addPlantUseCase;
            _updatePlantUseCase = updatePlantUseCase;
        }

        [HttpGet]
        [Authorize(Policy = PlantClaim.ViewPlant)]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _getAllPlantsUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PlantClaim.ViewPlant)]
        public IActionResult GetOne(int id)
        {
            return new OkObjectResult(_getOnePlantUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = PlantClaim.AddPlant)]
        public IActionResult Add([FromBody] PlantDTO plant)
        {
            return new OkObjectResult(_addPlantUseCase.Execute(_mapper.Map<PlantDTO, Plant>(plant)));
        }

        [HttpPut]
        [Authorize(Policy = PlantClaim.EditPlant)]
        [Authorize(Policy = PlantClaim.ActivatePlant)]
        [Authorize(Policy = PlantClaim.DeactivatePlant)]
        public IActionResult Update([FromBody] PlantDTO plant)
        {
            return new OkObjectResult(_updatePlantUseCase.Execute(_mapper.Map<PlantDTO, Plant>(plant)));
        }
    }
}