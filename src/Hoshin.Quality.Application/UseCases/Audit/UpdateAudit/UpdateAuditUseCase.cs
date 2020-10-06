using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Audit.UpdateAudit
{
    public  class UpdateAuditUseCase : IUpdateAuditUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAuditRepository _auditRepository;

        public UpdateAuditUseCase(IMapper mapper,IAuditRepository auditRepository)
        {
            _mapper = mapper;
            _auditRepository = auditRepository;
        }

        public bool Execute(Domain.Audit.Audit audit)
        {
            return _auditRepository.Update(audit);
        }
    }
}
