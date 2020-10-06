using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface ICorrectiveActionStateRepository
    {
        int GetByCode(string code);
    }
}
