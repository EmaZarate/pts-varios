using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.Quality.Application.UseCases.EditCorrectiveActionWorkgroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CorrectiveActionClaim = Hoshin.CrossCutting.Authorization.Claims.Quality.CorrectiveActions;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditCorrectiveActionWorkgroup
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EditCorrectiveActionWorkgroupController : ControllerBase
    {
        private readonly IEditCorrectiveActionWorkgroupUseCase _editCorrectiveActionWorkgroupUseCase;

        public EditCorrectiveActionWorkgroupController(IEditCorrectiveActionWorkgroupUseCase editCorrectiveActionWorkgroupUseCase)
        {
            _editCorrectiveActionWorkgroupUseCase = editCorrectiveActionWorkgroupUseCase;
        }

        [HttpPost]
        [Authorize(Policy = CorrectiveActionClaim.Planning)]
        public async Task<IActionResult> Add([FromBody] EditCorrectiveActionWorkgroupDTO model)
        {
            await _editCorrectiveActionWorkgroupUseCase.Execute(model.CorrectiveActionID, model.UsersID);
            return new OkObjectResult(true);
        }
    }
}