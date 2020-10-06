using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AuditType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllActivesAuditTypesUseCase
{
    public class GetAllActivesAuditTypesUseCase : IGetAllActivesAuditTypesUseCase
    {
        private readonly IAuditTypeRepository _auditTypeRepository;
        private readonly IMapper _mapper;

        public GetAllActivesAuditTypesUseCase(IAuditTypeRepository auditTypeRepository, IMapper mapper)
        {
            _auditTypeRepository = auditTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<AuditTypeOutput>> Execute()
        {
            var list = await _auditTypeRepository.GetAllActives();
            return _mapper.Map<List<AuditType>, List<AuditTypeOutput>>(list);
        }
    }
}
