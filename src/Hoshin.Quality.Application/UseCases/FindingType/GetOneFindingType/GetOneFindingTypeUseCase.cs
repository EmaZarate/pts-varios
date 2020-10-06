using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.GetOneFindingType
{
    public class GetOneFindingTypeUseCase : IGetOneFindingTypeUseCase
    {
        private readonly IFindingTypeRepository _findingTypeRepository;
        private readonly IMapper _mapper;

        public GetOneFindingTypeUseCase(IFindingTypeRepository findingTypeRepository, IMapper mapper)
        {
            _findingTypeRepository = findingTypeRepository;
            _mapper = mapper;
        }
        public FindingTypeOutput Execute(int id)
        {
            var res = _findingTypeRepository.Get(id);
            if(res != null)
            {
                return _mapper.Map<Domain.FindingType.FindingType, FindingTypeOutput>(res);
            }
            throw new EntityNotFoundException(id, "No se encontró un tipo de Hallazgo");
        }
    }
}
