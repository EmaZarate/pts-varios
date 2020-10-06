using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface ISectorPlantRepository
    {
        List<string> GetSectorPlantReferredEmail(int plantId, int sectorId);
    }
}
