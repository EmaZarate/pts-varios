using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditState.UpdateAuditState
{
    public class UpdateAuditStateUseCase : IUpdateAuditStateUseCase
    {
        private readonly IAuditStateRepository _auditStateRepository;

        public UpdateAuditStateUseCase(IAuditStateRepository auditStateRepository)
        {
            _auditStateRepository = auditStateRepository;
        }

        public AuditStateOutput Execute(Domain.AuditState.AuditState auditState)
        {
            var existAuditState = _auditStateRepository.Get(auditState.AuditStateID);

            var updatedAuditState = _auditStateRepository.Update(auditState);
            if (existAuditState != null)
            {
                return new AuditStateOutput() {
                    AuditStateID = updatedAuditState.AuditStateID,
                    Code = updatedAuditState.Code,
                    Name = updatedAuditState.Name,
                    Color = updatedAuditState.Color,
                    Active = updatedAuditState.Active
                };
            }
            else
            {
                throw new DuplicateEntityException(auditState.Name, "Ya existe un estado de auditoria con este nombre, código o color", 436);
            }

        }
    }
}
