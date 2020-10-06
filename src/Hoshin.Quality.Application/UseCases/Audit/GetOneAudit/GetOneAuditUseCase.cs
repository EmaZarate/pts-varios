using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Audit.GetOneAudit
{
    public class GetOneAuditUseCase : IGetOneAuditUseCase
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public GetOneAuditUseCase(IAuditRepository auditRepository, IMapper mapper)
        {
            _auditRepository = auditRepository;
            _mapper = mapper;
        }

        public AuditOutput Execute(int id)
        {
            var audit = _auditRepository.Get(id);
            return _mapper.Map<Domain.Audit.Audit, AuditOutput>(audit);
        }
    }
}
