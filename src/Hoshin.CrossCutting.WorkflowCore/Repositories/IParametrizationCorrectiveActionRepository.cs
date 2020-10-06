using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IParametrizationCorrectiveActionRepository
    {
        int GetByCode(string code);
    }
}
