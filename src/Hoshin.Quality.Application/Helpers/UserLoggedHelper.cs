using Hoshin.Core.Application.Repositories;
using Hoshin.Quality.Application.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Helpers
{
    public class UserLoggedHelper : IUserLoggedHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;

        private int userJobId;
        private int userSectorId;
        private int userPlantId;
        private string userId;
        public UserLoggedHelper(
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            ISectorPlantRepository sectorPlantRepository
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _sectorPlantRepository = sectorPlantRepository;
            userJobId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("job").Value);
            userSectorId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("sector").Value);
            userPlantId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("plant").Value);
            userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
        }

        private bool isReferrentOfSectorPlant()
        {
            var sectorPlant = _sectorPlantRepository.GetOne(userPlantId, userSectorId);

            return sectorPlant.ReferringJob == userJobId;
        }

        private bool isReferrent2OfSectorPlant()
        {
            var sectorPlant = _sectorPlantRepository.GetOne(userPlantId, userSectorId);

            return sectorPlant.ReferringJob2 == userJobId;
        }


        private int GetReferrentJobFromSectorPlantOfUserLogged()
        {
            return _sectorPlantRepository.GetOne(userPlantId, userSectorId).ReferringJob;
        }

        public bool CheckForPermissionsToUpdateReassignOrClose(string responsibleUser)
        {
            bool isReferred = isReferrentOfSectorPlant();
            bool isReferred2 = isReferrent2OfSectorPlant();

            bool isAsignedToResponsibleUser = userId == responsibleUser;


            if (!isAsignedToResponsibleUser)
            {
                int jobResponsibleUser = _userRepository.GetJobFromUser(responsibleUser);
                int sectorPlantReferedJob = GetReferrentJobFromSectorPlantOfUserLogged();
                if ((isReferred2 && (sectorPlantReferedJob == jobResponsibleUser)))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
