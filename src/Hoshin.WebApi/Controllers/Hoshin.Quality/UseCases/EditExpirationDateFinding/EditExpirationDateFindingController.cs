using AutoMapper;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.Quality.Application.UseCases.EditExpirationDateFinding;
using Hoshin.Quality.Domain.Finding;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditExpirationDateFinding
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class EditExpirationDateFindingController : ControllerBase
    {
        private readonly IEditExpirationDateFindingUseCase _editExpirationDateFindingUseCase;
        private readonly IMapper _mapper;

        public EditExpirationDateFindingController(IEditExpirationDateFindingUseCase editExpirationDateFindingUseCase, IMapper mapper)
        {
            _editExpirationDateFindingUseCase = editExpirationDateFindingUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = Findings.EditExpirationDate)]
        public IActionResult EditExpirationDateFinding([FromBody] EditExpirationDateFindingDTO editExpirationDateFindingDTO)
        {
            return new OkObjectResult(_editExpirationDateFindingUseCase.Execute(_mapper.Map<EditExpirationDateFindingDTO, Finding>(editExpirationDateFindingDTO), editExpirationDateFindingDTO.CreatedByUserId));
        }

    }
}