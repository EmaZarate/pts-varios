using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AuditType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.AddAuditTypeUseCase
{
    public class AddAuditTypeUseCase : IAddAuditTypeUseCase
    {
        private readonly IAuditTypeRepository _auditTypeRepository;
        private readonly IMapper _mapper;

        public AddAuditTypeUseCase (IAuditTypeRepository auditTypeRepository, IMapper mapper)
        {
            _auditTypeRepository = auditTypeRepository;
            _mapper = mapper;
        }

        public AuditTypeOutput Execute(AuditType auditType)
        {
            var tp = _auditTypeRepository.CheckDuplicated(auditType);
            if (tp == null)
            {
                return _mapper.Map<AuditType, AuditTypeOutput>(_auditTypeRepository.Add(auditType));
            }
            else
            {
                throw new DuplicateEntityException(auditType.Name, "Ya existe un tipo de auditoria con este nombre y/o código");
            }
        }
    }
}
