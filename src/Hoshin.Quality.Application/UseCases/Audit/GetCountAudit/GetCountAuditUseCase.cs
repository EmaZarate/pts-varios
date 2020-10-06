using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Application.Repositories;


namespace Hoshin.Quality.Application.UseCases.Audit.GetCountAudit
{
    public class GetCountAuditUseCase : IGetCountAuditUseCase
    {
        private readonly IAuditRepository _auditRepository;
        public GetCountAuditUseCase(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }
        public int Execute()
        {
            return _auditRepository.GetCount();
        }
    }
}
