using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CloseFinding
{
    public interface ICloseFindingUseCase
    {
        Task<bool> Execute(FindingWorkflowData finding);
    }
}
