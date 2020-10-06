using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditState.GetOneAuditState
{
    public class GetOneAuditStateUseCase : IGetOneAuditStateUseCase
    {
        private IAuditStateRepository _auditStateRepository;
        private readonly IMapper _mapper;

        public GetOneAuditStateUseCase(IAuditStateRepository auditStateRepository,
        IMapper mapper)
        {
            _auditStateRepository = auditStateRepository;
            _mapper = mapper;
        }


        public AuditStateOutput Execute(int id)
        {
            return _mapper.Map<Domain.AuditState.AuditState, AuditStateOutput>(_auditStateRepository.Get(id));
        }
    }
}
