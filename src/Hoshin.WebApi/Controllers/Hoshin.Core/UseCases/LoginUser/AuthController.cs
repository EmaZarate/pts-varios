using System;
using System.Threading.Tasks;
using Hoshin.Core.Application.UseCases.LoginUser;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.LoginUser
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class AuthController : ControllerBase
    {
        private readonly ILoginUserUseCase _loginUserUseCase;
        public AuthController(ILoginUserUseCase loginUserUseCase)
        {
            _loginUserUseCase = loginUserUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Hoshin([FromBody] CredentialsDTO credentials)
        {
            return new OkObjectResult(await _loginUserUseCase.Execute(credentials.UserName, credentials.Password, credentials.IsLocked));
        }

        [HttpPost]
        public async Task<IActionResult> MicrosoftGraph([FromBody] CredentialsDTO accessToken)
        {
            return new OkObjectResult(await _loginUserUseCase.Execute(accessToken.AccessToken));
        }
    }
}