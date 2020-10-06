using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Sector;

namespace Hoshin.Core.Application.UseCases.CRUDSector.GetOneSectorUseCase
{
    public class GetOneSectorUseCase : IGetOneSectorUseCase
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public GetOneSectorUseCase(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }
        public SectorOutput Execute(int id)
        {
            return _mapper.Map<Sector, SectorOutput>(_sectorRepository.GetOne(id));
        }
    }
}
