using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface ICorrectiveActionEvidenceRepository
    {
        bool Update(int correctiveActionId, List<string> addUrls, List<string> deleteUrls);
    }
}
