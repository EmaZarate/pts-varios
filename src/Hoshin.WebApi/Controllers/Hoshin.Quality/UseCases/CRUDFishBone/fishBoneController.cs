using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Hoshin.WebApi.Filters;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.FishBone.GetAll;
using Hoshin.Quality.Application.UseCases.FishBone.UpdateFishBone;
using Hoshin.Quality.Application.UseCases.FishBone.GetOneFishBone;
using Hoshin.Quality.Application.UseCases.FishBone.CreateFishBone;
using Hoshin.Quality.Application.UseCases.FishBone.GetAllActive;
using FishBoneCategoryClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.FishBoneCategory;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFishBone
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class FishBoneController : Controller
    {
        private readonly IGetAllFishBoneUseCase _getAllFishBoneUseCase;
        private readonly IUpdateFishBoneUseCase _updateFishBoneUseCase;
        private readonly IGetOneFishBoneUseCase _getOneFishBoneUseCase;
        private readonly ICreateFishBoneUseCase _createFishBoneUseCase;
        private readonly IGetAllActiveFishBoneUseCase _getAllActiveFishBoneUseCase;
        public FishBoneController(
            IGetAllFishBoneUseCase getAllFishBoneUseCase,
            IUpdateFishBoneUseCase updateFishBoneUseCase,
            IGetOneFishBoneUseCase getOneFishBoneUseCase,
            ICreateFishBoneUseCase createFishBoneUseCase,
            IGetAllActiveFishBoneUseCase getAllActiveFishBoneUseCase
            )
        {
            _getAllFishBoneUseCase = getAllFishBoneUseCase;
            _updateFishBoneUseCase = updateFishBoneUseCase;
            _getOneFishBoneUseCase = getOneFishBoneUseCase;
            _createFishBoneUseCase = createFishBoneUseCase;
            _getAllActiveFishBoneUseCase = getAllActiveFishBoneUseCase;
        }

        [HttpGet]
        [Authorize(Policy = FishBoneCategoryClaim.ReadFishBoneCategory)]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllFishBoneUseCase.Execute());
        }

        [HttpGet]
        [Authorize(Policy = FishBoneCategoryClaim.ReadFishBoneCategory)]
        public IActionResult GetAllActive()
        {
            return new OkObjectResult(_getAllActiveFishBoneUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = FishBoneCategoryClaim.ReadFishBoneCategory)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneFishBoneUseCase.Execute(id));
        }
        [HttpPut]
        [Authorize(Policy = FishBoneCategoryClaim.EditFishBoneCategory)]
        public IActionResult Update([FromBody] FishBoneDTO model)
        {
            return new OkObjectResult(_updateFishBoneUseCase.Execute(model.FishboneID, model.Name, model.Color, model.Active));
        }

        [HttpPost]
        [Authorize(Policy = FishBoneCategoryClaim.AddFishBoneCategory)]
        public IActionResult Add([FromBody] FishBoneDTO model)
        {
            return new OkObjectResult(_createFishBoneUseCase.Execute(model.Active, model.Color, model.Name));
        }

    }
}
