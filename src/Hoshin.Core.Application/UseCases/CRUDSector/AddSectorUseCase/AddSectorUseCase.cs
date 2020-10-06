using AutoMapper;
using Hoshin.Core.Application.Exceptions.Sector;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Sector;

namespace Hoshin.Core.Application.UseCases.CRUDSector.AddSectorUseCase
{
    public class AddSectorUseCase : IAddSectorUseCase
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;
        public AddSectorUseCase(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }
        public SectorOutput Execute(Sector sector)
        {
            var s = _sectorRepository.CheckDuplicated(sector);
            if (s == null)
            {
                return _mapper.Map<Sector, SectorOutput>(_sectorRepository.Add(sector));
            }
            else
            {
                throw new SectorWithThisNameAndOrCodeAlreadyExists(sector.Name, sector.Code, "Ya existe un sector con este Nombre y/o con este código");
            }
        }
    }
}
