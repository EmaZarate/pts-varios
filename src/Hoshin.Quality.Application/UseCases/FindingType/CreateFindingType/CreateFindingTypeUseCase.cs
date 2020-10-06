using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.FindingType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.CreateFindingType
{
    public class CreateFindingTypeUseCase : ICreateFindingTypeUseCase
    {
        private readonly IFindingTypeRepository _findingTypeRepository;

        public CreateFindingTypeUseCase(IFindingTypeRepository findingTypeRepository)
        {
            _findingTypeRepository = findingTypeRepository;
        }
        public FindingTypeOutput Execute(string name, string code, bool active, List<FindingTypeParametrization> parametrization)
        {
            var res = _findingTypeRepository.Get(name);
            if(res == null)
            {
                var ftAdded = _findingTypeRepository.Add(new Domain.FindingType.FindingType(name, code, active, parametrization));
                return new FindingTypeOutput(ftAdded.Id, ftAdded.Name, ftAdded.Code, ftAdded.Active, ftAdded.Parametrizations);
            }
            throw new DuplicateEntityException(name, "Ya existe un tipo de Hallazgo con ese nombre.");
        }
    }
}
