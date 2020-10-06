using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.GetAllForAuditFindingType
{
    public class GetAllForAuditFindingTypeUseCase : IGetAllForAuditFindingTypeUseCase
    {
        private readonly IFindingTypeRepository _findingTypeRepository;
        private readonly IMapper _mapper;

        public GetAllForAuditFindingTypeUseCase(IFindingTypeRepository findingTypeRepository, IMapper mapper)
        {
            _findingTypeRepository = findingTypeRepository;
            _mapper = mapper;
        }
        public List<FindingTypeOutput> Execute() => 
            _mapper.Map<List<Domain.FindingType.FindingType>, List<FindingTypeOutput>>(_findingTypeRepository.GetAllForAudit());
    }
}
