using Hoshin.Core.Domain.Sector;

namespace Hoshin.Core.Application.UseCases.CRUDSector.UpdateSectorUseCase
{
    public interface IUpdateSectorUseCase
    {
        SectorOutput Execute(Sector sector);
    }
}