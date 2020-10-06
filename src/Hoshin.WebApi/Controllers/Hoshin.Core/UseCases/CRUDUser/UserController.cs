using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.Core.Application.UseCases.User.GetAllUser;
using Hoshin.Core.Application.UseCases.User.GetOneUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hoshin.WebApi.Filters;
using Hoshin.Core.Application.UseCases.User.AddUser;
using Hoshin.Core.Application.UseCases.User.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using UserClaim = Hoshin.CrossCutting.Authorization.Claims.Core.User;
using Hoshin.Core.Application.UseCases.User.ResetPasswordUser;
using Hoshin.Core.Application.Repositories;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDUser
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class UserController : ControllerBase
    {
        private readonly IGetAllUserUseCase _getAllUserUseCase;
        private readonly IGetOneUserUseCase _getOneUserUseCase;
        private readonly IAddUserUseCase _addUserUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly IResetPasswordUseCase _resetPasswordUseCase;
        private readonly IUserRepository _userRepository;

        public UserController(
            IGetAllUserUseCase getAllUserUseCase, 
            IGetOneUserUseCase getOneUserUseCase, 
            IAddUserUseCase addUserUseCase,
            IUpdateUserUseCase updateUserUseCase,
            IResetPasswordUseCase resetPasswordUseCase,
            IUserRepository userRepository
            )
        {
            _getAllUserUseCase = getAllUserUseCase;
            _getOneUserUseCase = getOneUserUseCase;
            _addUserUseCase = addUserUseCase;
            _updateUserUseCase = updateUserUseCase;
            _resetPasswordUseCase = resetPasswordUseCase;
            _userRepository = userRepository;
        }

        [HttpGet("{id_sector}/{id_plant}")]
        [Authorize(Policy = UserClaim.ViewUser)]
        //[ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> Get(int id_sector, int id_plant)
        {
            return new OkObjectResult(await _getAllUserUseCase.Execute(id_sector, id_plant));
        }

        [HttpGet]
        [Authorize(Policy = UserClaim.ViewUser)]
        public IActionResult Get()
        {
            return new OkObjectResult(_getAllUserUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserClaim.ViewUser)]
        public async Task<IActionResult> GetOne(string id)
        {
            return new OkObjectResult(await _getOneUserUseCase.Execute(id));
        }
        [HttpGet("{id}")]
        [Authorize(Policy = UserClaim.ViewUser)]
        public async Task<IActionResult> GetRoles(string id)
        {
            return new OkObjectResult(await _userRepository.GetRoles(id));
        }

        [HttpPost]
        [Authorize(Policy = UserClaim.AddUser)]
        public async Task<IActionResult> Add([FromBody] UserDTO user)
        {
            return new OkObjectResult(await _addUserUseCase.Execute(user.Username, user.Password, user.JobID, user.SectorID, user.PlantID, user.Firstname, user.Surname, user.Roles, user.Active));
        }


        [HttpPut]
        [Authorize(Policy = UserClaim.EditProfile)]
        //[Authorize(Policy = UserClaim.ActivateUser)]
        //[Authorize(Policy = UserClaim.DeactivateUser)]
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            return new OkObjectResult(await _updateUserUseCase.Execute(User, user.Id, user.Username, user.Password, user.JobID, user.SectorID, user.PlantID, user.Firstname, user.Surname, user.Roles,user.Address,user.PhoneNumber, user.Base64Profile, user.Active));
        }

        [HttpPost]
        [Authorize(Policy = UserClaim.ViewUser)]
        public async Task<IActionResult> ResetPassword([FromBody] UserDTO user)
        {
            await _resetPasswordUseCase.Execute(user.Password, user.Id);
            return new OkResult();
        }
    }
}