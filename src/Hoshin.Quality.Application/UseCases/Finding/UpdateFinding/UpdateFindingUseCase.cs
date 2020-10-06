using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Finding.UpdateFinding
{
    public class UpdateFindingUseCase : IUpdateFindingUseCase
    {
        private readonly IFindingRepository _findingRepository;

        public UpdateFindingUseCase(IFindingRepository findingRepository)
        {
            _findingRepository = findingRepository;
        }

        public bool Execute(Domain.Finding.Finding finding)
        {
            var oldFinding = _findingRepository.GetWithoutIncludes(finding.Id);

            if(oldFinding == null)
            {
                throw new EntityNotFoundException(finding.Id, "No se encontro un hallazgo con ese nombre");
            }

            oldFinding.Description = finding.Description;
            oldFinding.FindingTypeID = finding.FindingTypeID;

            return _findingRepository.Update(oldFinding);
        }
    }
}
