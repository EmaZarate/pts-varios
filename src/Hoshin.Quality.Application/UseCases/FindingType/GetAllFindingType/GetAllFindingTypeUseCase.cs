using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.GetAllFindingType
{
    public class GetAllFindingTypeUseCase : IGetAllFindingTypeUseCase
    {
        private readonly IFindingTypeRepository _findingTypeRepository;
        private readonly IMapper _mapper;
        public GetAllFindingTypeUseCase(IFindingTypeRepository findingTypeRepository, IMapper mapper)
        {
            _findingTypeRepository = findingTypeRepository;
            _mapper = mapper;
        }
        public List<FindingTypeOutput> Execute()
        {
            var list = _mapper.Map<List<Domain.FindingType.FindingType>, List<FindingTypeOutput>>(_findingTypeRepository.GetAll());
            return list;
        }
    }
}
