using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.FindingType;

namespace Hoshin.Quality.Application.UseCases.FindingType.UpdateFindingType
{
    public class UpdateFindingTypeUseCase : IUpdateFindingTypeUseCase
    {
        private readonly IFindingTypeRepository _findingTypeRepository;
        private readonly IMapper _mapper;

        public UpdateFindingTypeUseCase(IFindingTypeRepository findingTypeRepository, IMapper mapper)
        {
            _findingTypeRepository = findingTypeRepository;
            _mapper = mapper;
        }
        public FindingTypeOutput Execute(Domain.FindingType.FindingType updateFindingType)
        {
            var findingtype = _findingTypeRepository.Get(updateFindingType.Id);
            if(findingtype != null)
            {
                var validateName = _findingTypeRepository.Get(updateFindingType.Name);
                if (validateName == null || validateName.Id == updateFindingType.Id)
                {
                    var res = _findingTypeRepository.Update(updateFindingType);
                    return _mapper.Map<Domain.FindingType.FindingType, FindingTypeOutput>(res);
                }
                else
                {
                    throw new DuplicateEntityException(updateFindingType.Name, "Ya existe un Tipo de hallazgo con ese nombre");
                }


            }
            throw new EntityNotFoundException(updateFindingType.Id, "El tipo de Hallazgo no existe.");

        }
    }
}
