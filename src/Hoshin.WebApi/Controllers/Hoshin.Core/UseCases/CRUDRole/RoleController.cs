using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoshin.Core.Application.UseCases.Role.AddRole;
using Hoshin.Core.Application.UseCases.Role.CheckIfNameExists;
using Hoshin.Core.Application.UseCases.Role.CheckIfBasicExists;
using Hoshin.Core.Application.UseCases.Role.GetAllRole;
using Hoshin.Core.Application.UseCases.Role.GetAllRolesActive;
using Hoshin.Core.Application.UseCases.Role.GetRole;
using Hoshin.Core.Application.UseCases.Role.UpdateRole;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleClaim = Hoshin.CrossCutting.Authorization.Claims.Core.Roles;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDRole
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class RoleController : ControllerBase
    {
        private readonly IAddRoleUseCase _addRoleUseCase;
        private readonly ICheckIfNameExistsUseCase _checkIfNameExistsUseCase;
        private readonly ICheckIfBasicExistsUseCase _checkIfBasicExistsUseCase;
        private readonly IGetOneRoleUseCase _getOneRoleUseCase;
        private readonly IUpdateRoleUseCase _updateRoleUseCase;
        private readonly IGetAllRolesUseCase _getAllRolesUseCase;
        private readonly IGetAllRolesActiveUseCase _getAllRolesActiveUseCase;
        public RoleController(
            IAddRoleUseCase addRoleUseCase, 
            ICheckIfNameExistsUseCase checkIfNameExistsUseCase,
            ICheckIfBasicExistsUseCase checkIfBasicExistsUseCase,
            IGetOneRoleUseCase getOneRoleUseCase, 
            IUpdateRoleUseCase updateRoleUseCase,
            IGetAllRolesUseCase getAllRolesUseCase,
            IGetAllRolesActiveUseCase getAllRolesActiveUseCase
            )
        {
            _addRoleUseCase = addRoleUseCase;
            _checkIfNameExistsUseCase = checkIfNameExistsUseCase;
            _getOneRoleUseCase = getOneRoleUseCase;
            _updateRoleUseCase = updateRoleUseCase;
            _getAllRolesUseCase = getAllRolesUseCase;
            _checkIfBasicExistsUseCase = checkIfBasicExistsUseCase;
            _getAllRolesActiveUseCase = getAllRolesActiveUseCase;
        }

        [HttpGet]
        [Authorize(Policy = RoleClaim.ViewRole)]
       // [ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _getAllRolesUseCase.Execute());
        }

        [HttpGet]
        [Authorize(Policy = RoleClaim.ViewRole)]
      //  [ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> GetAllRolesActive()
        {
            return new OkObjectResult(await _getAllRolesActiveUseCase.Execute());
        }

        [HttpPost]
        [Authorize(Policy = RoleClaim.AddRole)]
        public async Task<IActionResult> Add([FromBody] RoleDTO role)
        {
            return new OkObjectResult(await _addRoleUseCase.Execute(role.Name, role.Claims, role.Active, role.Basic));
        }

        [HttpGet("{name}")]
        [Authorize(Policy = RoleClaim.AddRole)]
        public async Task<IActionResult> Check(string name)
        {
            return new OkObjectResult(await _checkIfNameExistsUseCase.Execute(name));
        }

        [HttpGet]
        [Authorize(Policy = RoleClaim.AddRole)]
        public async Task<IActionResult> CheckBasic()
        {
            return new OkObjectResult(await _checkIfBasicExistsUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = RoleClaim.ViewRole)]
        public async Task<IActionResult> Get(string id)
        {
            return new OkObjectResult(await _getOneRoleUseCase.Execute(id));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = RoleClaim.EditRole)]
        [Authorize(Policy = RoleClaim.ActivateRole)]
        [Authorize(Policy = RoleClaim.DeactivateRoles)]
        public async Task<IActionResult> Update([FromBody] RoleDTO role, string id)
        {
            return new OkObjectResult(await _updateRoleUseCase.Execute(id, role.Name, role.Claims, role.Active, role.Basic));
        }
    }
}