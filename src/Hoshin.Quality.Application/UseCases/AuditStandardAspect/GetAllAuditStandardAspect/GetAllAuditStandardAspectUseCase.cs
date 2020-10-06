using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditStandardAspect.GetAllAuditStandardAspect
{
    public class GetAllAuditStandardAspectUseCase: IGetAllAuditStandardAspectUseCase
    {
        private readonly IAuditStandardAspectRepository _auditStandardAspectRepository;
        private readonly IMapper _mapper;
        public GetAllAuditStandardAspectUseCase(IAuditStandardAspectRepository auditStandardAspectRepository, IMapper mapper)
        {
            _auditStandardAspectRepository = auditStandardAspectRepository;
            _mapper = mapper;
        }

        public List<AuditStandardAspectOutput> Execute(int id)
        {
            return _mapper.Map<List<Hoshin.Quality.Domain.AuditStandardAspect>, List<AuditStandardAspectOutput>> (_auditStandardAspectRepository.GetAllforAudit(id));
        }
    }

}
