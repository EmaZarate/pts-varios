using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IAuditStateRepository
    {
        int GetOneByCode(string code);
    }
}
