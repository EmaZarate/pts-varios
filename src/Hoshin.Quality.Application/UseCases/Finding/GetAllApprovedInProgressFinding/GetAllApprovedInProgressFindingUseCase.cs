using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Finding.GetAllApprovedInProgressFinding
{
    public class GetAllApprovedInProgressFinding : IGetAllApprovedInProgressFindingUseCase
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IMapper _mapper;

        public GetAllApprovedInProgressFinding(IFindingRepository findingRepository, IMapper mapper)
        {
            _findingRepository = findingRepository;
            _mapper = mapper;
        }

        public List<FindingOutput> Execute()
        {
            var findings = _findingRepository.GetAllApprovedInProgress();
            return _mapper.Map<List<Domain.Finding.Finding>, List<FindingOutput>>(findings);
        }
    }
}
