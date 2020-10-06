using Hoshin.Core.Domain.Sector;

namespace Hoshin.Core.Application.UseCases.CRUDSector.AddSectorUseCase
{
    public interface IAddSectorUseCase
    {
        SectorOutput Execute(Sector sector);
    }
}