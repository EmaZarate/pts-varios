using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditState.CreateAuditState
{
    public class CreateAuditStateUseCase : ICreateAuditStateUseCase
    {
        private readonly IAuditStateRepository _auditStateRepository;
        private readonly IMapper _mapper;


        public CreateAuditStateUseCase(IAuditStateRepository auditStateRepository,IMapper mapper)
        {
            _auditStateRepository = auditStateRepository;
            _mapper = mapper;
        }


        public AuditStateOutput Execute(Domain.AuditState.AuditState auditState)
        {
            var newAuditState = _auditStateRepository.Add(auditState);
            if (newAuditState != null)
            {
                return new AuditStateOutput()
                {
                    AuditStateID = newAuditState.AuditStateID,
                    Code = newAuditState.Code,
                    Name = newAuditState.Name,
                    Color = newAuditState.Color,
                    Active = newAuditState.Active
                };
            }
            else
            {
                throw new DuplicateEntityException(auditState.Name, "Ya existe un estado de auditoria con este nombre, código o color", 436);
            }

        }
    }
}
