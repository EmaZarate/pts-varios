using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AuditType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllAuditTypesUseCase
{
    public class GetAllAuditTypesUseCase : IGetAllAuditTypesUseCase
    {
        private readonly IAuditTypeRepository _auditTypeRepository;
        private readonly IMapper _mapper;

        public GetAllAuditTypesUseCase(IAuditTypeRepository auditTypeRepository, IMapper mapper)
        {
            _auditTypeRepository = auditTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<AuditTypeOutput>> Execute()
        {
            var list = await _auditTypeRepository.GetAll();
            return _mapper.Map<List<AuditType>, List<AuditTypeOutput>>(list);
        }
    }
}
