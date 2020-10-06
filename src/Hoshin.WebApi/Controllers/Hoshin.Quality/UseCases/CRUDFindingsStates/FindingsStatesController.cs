using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.FindingsStates.CreateFindingsStateUseCase;
using Hoshin.Quality.Application.UseCases.FindingsStates.GetAllFindingsStates;
using Hoshin.Quality.Application.UseCases.FindingsStates.GetOneFindingsStates;
using Hoshin.Quality.Application.UseCases.FindingsStates.UpdateFindingsStates;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFindingsStates
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class FindingsStatesController : ControllerBase
    {
        private readonly IGetAllFindingsStatesUseCase _getAllFindingsStatesUseCase;
        private readonly IGetOneFindingsStatesUseCase _getOneFindingsStatesUseCase;
        private readonly ICreateFindingsStateUseCase _createFindingsStateUseCase;
        private readonly IUpdateFindingsStatesUseCase _updateFindingsStatesUseCase;
        
        

        public FindingsStatesController(
                IGetAllFindingsStatesUseCase getAllFindingsStatesUseCase,
                IGetOneFindingsStatesUseCase getOneFindingsStatesUseCase,
                ICreateFindingsStateUseCase createFindingsStateUseCase,
                IUpdateFindingsStatesUseCase updateFindingsStateUseCasa
            )
        {
            _getAllFindingsStatesUseCase = getAllFindingsStatesUseCase;
            _getOneFindingsStatesUseCase = getOneFindingsStatesUseCase;
            _createFindingsStateUseCase = createFindingsStateUseCase;
            _updateFindingsStatesUseCase = updateFindingsStateUseCasa;
        }

        [HttpGet]
        [Authorize(Policy = FindingStates.ReadStates)]
       // [ServiceFilter(typeof(CacheEndpointFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllFindingsStatesUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = FindingStates.ReadStates)]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_getOneFindingsStatesUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = FindingStates.AddStates)]
        public IActionResult Add([FromBody] FindingsStatesDTO model)
        {
            return new OkObjectResult(_createFindingsStateUseCase.Execute(model.Code, model.Name, model.Colour, model.Active));
        }

        [HttpPut]
        [Authorize(Policy = FindingStates.EditStates)]
        //[Authorize(Policy = FindingStates.ActivateStates)]
        //[Authorize(Policy = FindingStates.DeactivateStates)]
        public IActionResult Update([FromBody] FindingsStatesDTO model)
        {
            return new OkObjectResult(_updateFindingsStatesUseCase.Execute(model.Id, model.Code, model.Name, model.Colour, model.Active));
        }
    }
}