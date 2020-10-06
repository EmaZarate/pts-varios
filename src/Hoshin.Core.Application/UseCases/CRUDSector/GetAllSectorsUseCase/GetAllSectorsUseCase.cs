using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Sector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDSector.GetAllSectorsUseCase
{
    public class GetAllSectorsUseCase : IGetAllSectorsUseCase
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;
        public GetAllSectorsUseCase(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }
        public async Task<List<SectorOutput>> Execute()
        {
            var list = await _sectorRepository.GetAll();
            return _mapper.Map<List<Sector>, List<SectorOutput>>(list);
        }
    }
}
