using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AuditType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.UpdateAuditTypeUseCase
{
    public class UpdateAuditTypeUseCase : IUpdateAuditTypeUseCase
    {
        private readonly IAuditTypeRepository _auditTypeRepository;
        private readonly IMapper _mapper;

        public UpdateAuditTypeUseCase(IAuditTypeRepository auditTypeRepository, IMapper mapper)
        {
            _auditTypeRepository = auditTypeRepository;
            _mapper = mapper;
        }

        public AuditTypeOutput Execute(AuditType auditType)
        {
            var at = _auditTypeRepository.CheckDuplicated(auditType);
            if (at == null)
            {
                return _mapper.Map<AuditType, AuditTypeOutput>(_auditTypeRepository.Update(auditType));
            }
            else
            {
                throw new DuplicateEntityException(auditType.Code, "Ya existe un tipo de auditoria con este código y/o nombre");
            }
        }
    }
}
