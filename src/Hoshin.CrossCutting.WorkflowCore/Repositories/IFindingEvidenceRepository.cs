using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IFindingEvidenceRepository
    {
        bool Add(int findingId, string url);
        bool Delete(int findingId, string url);
    }
}
