using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Authorization.Claims;
using Hoshin.Quality.Application.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Finding.GetAllFinding
{
    public class GetAllFindingUseCase : IGetAllFindingUseCase
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IReassignmentsFindingHistoryRepository _reassignmentsFindingHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public GetAllFindingUseCase(
            IFindingRepository findingRepository, 
            IMapper mapper, 
            IReassignmentsFindingHistoryRepository reassignmentsFindingHistoryRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _findingRepository = findingRepository;
            _mapper = mapper;
            _reassignmentsFindingHistoryRepository = reassignmentsFindingHistoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<List<FindingOutput>> Execute()
        {
            List<Domain.Finding.Finding> findings;
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var userPlantId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("plant").Value);
            var userSectorId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("sector").Value);

            if (_httpContextAccessor.HttpContext.User.HasClaim(CustomClaimTypes.Permission, CrossCutting.Authorization.Claims.Quality.Findings.Approve))
            {
                findings = _findingRepository.GetAll();
            }
            else if(await _userRepository.isColaboratorOrSectorBoss(userId))
            {
                findings = _findingRepository.GetAllFromSectorPlant(userPlantId, userSectorId);
            }
            else
            {
                findings = _findingRepository.GetAllFromUser(userId, userPlantId, userSectorId);
            }

            return _mapper.Map<List<Domain.Finding.Finding>, List<FindingOutput>>(findings);

            /*---------------- Se comentó porque no se estaba usando en ningún lado ----------------*/
            //var fidnignsOutputList = new List<FindingOutput>();
            //foreach (var finding in findings)
            //{
            //    var fidnignsOutput = _mapper.Map<Domain.Finding.Finding, FindingOutput> (finding);
            //    var lastReassignment = _reassignmentsFindingHistoryRepository.GetLast(finding.Id);
            //    if (lastReassignment != null)
            //    {
            //        fidnignsOutput.FindingsReassignmentsHistoryState = lastReassignment.State;
            //    }    
            //    fidnignsOutputList.Add(fidnignsOutput);
            //}

            //return fidnignsOutputList;
        }
    }
}
