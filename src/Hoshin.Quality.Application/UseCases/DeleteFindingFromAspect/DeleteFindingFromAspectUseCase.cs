using Hoshin.Quality.Application.Exceptions.Finding;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.DeleteFindingFromAspect
{
    public class DeleteFindingFromAspectUseCase : IDeleteFindingFromAspectUseCase
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IAuditStandardAspectRepository _auditStandardAspectRepository;

        public DeleteFindingFromAspectUseCase(IFindingRepository findingRepository, IAuditStandardAspectRepository auditStandardAspectRepository)
        {
            _findingRepository = findingRepository;
            _auditStandardAspectRepository = auditStandardAspectRepository;
        }
        public bool Execute(int id)
        {
            var findingToDelete = _findingRepository.GetWithoutIncludes(id);

            if(findingToDelete.AuditID == null)
            {
                throw new CantDeleteFindingException(findingToDelete);
            }

            var auditStandardAspect = _auditStandardAspectRepository.Get(findingToDelete.AuditID.Value, findingToDelete.StandardID.Value, findingToDelete.AspectID.Value);
            if(auditStandardAspect.Findings.Count == 1 && auditStandardAspect.Findings.Select(x => x.Id).Contains(findingToDelete.Id))
            {
                _auditStandardAspectRepository.SetPendingState(auditStandardAspect);
            }

            _findingRepository.Delete(id);

            return true;
        }
    }
}
