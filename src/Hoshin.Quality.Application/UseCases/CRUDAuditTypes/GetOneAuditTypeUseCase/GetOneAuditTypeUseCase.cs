using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AuditType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetOneAuditTypeUseCase
{
    public class GetOneAuditTypeUseCase : IGetOneAuditTypeUseCase
    {
        private readonly IAuditTypeRepository _auditTypeRepository;
        private readonly IMapper _mapper;

        public GetOneAuditTypeUseCase(IAuditTypeRepository auditTypeRepository, IMapper mapper)
        {
            _auditTypeRepository = auditTypeRepository;
            _mapper = mapper;
        }

        public AuditTypeOutput Execute(int id)
        {
            return _mapper.Map<AuditType, AuditTypeOutput>(_auditTypeRepository.Get(id));
        }
    }
}
