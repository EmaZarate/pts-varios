using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.UpdateApprovedFinding
{
    public interface IUpdateApprovedFindingUseCase
    {
        Task<bool> Execute(FindingWorkflowData finding, IFormFile[] findingEvidences, List<string> filesToDelete);
    }
}
