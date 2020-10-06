using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;

namespace Hoshin.Quality.Application.UseCases.ReassignFinding.GetLastReassignment
{
    public class GetLastReassignment:IGetLastReassignment
    {
        private readonly IReassignmentsFindingHistoryRepository _reassignmentsFindingHistoryRepository;
        private readonly IMapper _mapper;

        public GetLastReassignment(IReassignmentsFindingHistoryRepository reassignmentFindingHistoryRepository, IMapper mapper)
        {
            _reassignmentsFindingHistoryRepository = reassignmentFindingHistoryRepository;
            _mapper = mapper;
        }

        public ReassignmentsFindingHistoryOutput Execute(int id_finding)
        {
            ReassignmentsFindingHistory reassignmentsFindingHistory = _reassignmentsFindingHistoryRepository.GetLast(id_finding);
            return _mapper.Map<ReassignmentsFindingHistory, ReassignmentsFindingHistoryOutput>(reassignmentsFindingHistory);
        }
    }
}
