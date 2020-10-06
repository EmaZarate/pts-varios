using Hoshin.Quality.Domain.FindingType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IFindingTypeRepository
    {
        FindingType Get(string name);
        FindingType Get(int id);
        FindingType Add(FindingType findingType);
        List<FindingType> GetAll();
        List<FindingType> GetAllActive();
        List<FindingType> GetAllForAudit();
        FindingType Update(FindingType updateFindingType);
        bool Delete(int id);
    }
}
