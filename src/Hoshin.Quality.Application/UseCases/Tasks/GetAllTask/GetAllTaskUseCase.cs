using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Authorization.Claims;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Task;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Tasks.GetAllTask
{
    public class GetAllTaskUseCase : IGetAllTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public GetAllTaskUseCase(ITaskRepository taskRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._taskRepository = taskRepository;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public List<TaskOutput> Execute(int id)
        {
            var listOutput = new List<TaskOutput>();
            var listEntities = _taskRepository.GetAllForCorrectiveAction(id);
            return _mapper.Map<List<Domain.Task.Task>, List<TaskOutput>>(listEntities);
        }

        public async Task<List<TaskOutput>> Execute()
        {
            try
            {
                var listEntities = new List<Domain.Task.Task>();
                var userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
                var userPlantId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("plant").Value);
                var userSectorId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("sector").Value);
                if(_httpContextAccessor.HttpContext.User.HasClaim(CustomClaimTypes.Permission, CrossCutting.Authorization.Claims.Quality.CorrectiveActions.Delete))
                {
                    listEntities = _taskRepository.GetAll();
                }
                else if (await _userRepository.isColaborator(userId))
                {
                    listEntities = _taskRepository.GetAllFromUserAndReferring(userId);
                }
                else
                {
                    listEntities = _taskRepository.GetAllFromUser(userId);
                }
                
                return _mapper.Map<List<Domain.Task.Task>, List<TaskOutput>>(listEntities);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
