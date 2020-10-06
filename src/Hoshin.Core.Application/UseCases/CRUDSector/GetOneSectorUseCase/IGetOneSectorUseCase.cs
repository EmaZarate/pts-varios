namespace Hoshin.Core.Application.UseCases.CRUDSector.GetOneSectorUseCase
{
    public interface IGetOneSectorUseCase
    {
        SectorOutput Execute(int id);
    }
}