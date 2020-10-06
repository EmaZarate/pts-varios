using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Authorization.Claims;
using Hoshin.Quality.Application.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.GetAllCorrectiveAction
{
    public class GetAllCorrectiveActionsUseCase : IGetAllCorrectiveActionsUseCase
    {
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public GetAllCorrectiveActionsUseCase(
            ICorrectiveActionRepository correctiveActionRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _correctiveActionRepository = correctiveActionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<List<CorrectiveActionOutput>> Execute()
        {
            var correctiveActions = new List<Domain.CorrectiveAction.CorrectiveAction>();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var userPlantId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("plant").Value);
            var userSectorId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("sector").Value);

            if (_httpContextAccessor.HttpContext.User.HasClaim(CustomClaimTypes.Permission, CrossCutting.Authorization.Claims.Quality.CorrectiveActions.Delete))
            {
                correctiveActions = _correctiveActionRepository.GetAll();
            }
            else if (await _userRepository.isColaboratorOrSectorBoss(userId))
            {
                correctiveActions = _correctiveActionRepository.GetAllFromSectorPlant(userPlantId, userSectorId);
            }
            else
            {
                correctiveActions = _correctiveActionRepository.GetAllFromUser(userId, userPlantId, userSectorId);
            }
            return _mapper.Map<List<Domain.CorrectiveAction.CorrectiveAction>, List<CorrectiveActionOutput>>(correctiveActions);
        }
    }
}
