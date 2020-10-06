using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditStandardAspect.SetNoAudited
{
    public class SetNoAuditedAuditStandardAspectUseCase : ISetNoAuditedAuditStandardAspectUseCase
    {
        private readonly IAuditStandardAspectRepository _auditStandardAspectRepository;

        public SetNoAuditedAuditStandardAspectUseCase(IAuditStandardAspectRepository auditStandardAspectRepository)
        {
            _auditStandardAspectRepository = auditStandardAspectRepository;
        }
        public bool Execute(Domain.AuditStandardAspect auditStandardAspect)
        {
            _auditStandardAspectRepository.SetNoAudited(auditStandardAspect);

            return true;
        }
    }
}
