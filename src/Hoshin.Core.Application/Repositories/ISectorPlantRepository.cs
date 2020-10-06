using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.Repositories
{
    public interface ISectorPlantRepository
    {
        Domain.SectorPlant GetOne(int idPlant, int idSector);
        List<string> GetSectorPlantReferredEmail(int plantId, int sectorId);
    }
}
