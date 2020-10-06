using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditState.GetAllAuditState
{
    public class GetAllAuditStateUseCase : IGetAllAuditStateUseCase
    {
        private readonly IAuditStateRepository _auditStateRepository;
        private readonly IMapper _mapper;

        public GetAllAuditStateUseCase(IAuditStateRepository auditStateRepository,
            IMapper mapper)
        {
            _auditStateRepository = auditStateRepository;
            _mapper = mapper;
        }

        public List<AuditStateOutput> Execute()
        {
            return _mapper.Map<List<Domain.AuditState.AuditState>, List<AuditStateOutput>>(_auditStateRepository.GetAll());
        }
    }
}
