using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IFindingStateRepository
    {
        int GetOneByCode(string code);
    }
}
