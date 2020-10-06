using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditStandardAspect.SetWithoutFindings
{
    public class SetWithoutFindingsAuditStandardAspectUseCase : ISetWithoutFindingsAuditStandardAspectUseCase
    {
        private readonly IAuditStandardAspectRepository _auditStandardAspectRepository;

        public SetWithoutFindingsAuditStandardAspectUseCase(IAuditStandardAspectRepository auditStandardAspectRepository)
        {
            _auditStandardAspectRepository = auditStandardAspectRepository;
        }
        public bool Execute(Domain.AuditStandardAspect auditStandardAspect)
        {
            _auditStandardAspectRepository.SetWithoutFinding(auditStandardAspect);

            return true;
        }
    }
}
