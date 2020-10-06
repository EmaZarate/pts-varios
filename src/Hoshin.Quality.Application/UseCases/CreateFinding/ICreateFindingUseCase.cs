using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CreateFinding
{
    public interface ICreateFindingUseCase
    {
        Task<FindingWorkflowData> Execute(FindingWorkflowData finding, IFormFile[] findingEvidences);
    }
}
