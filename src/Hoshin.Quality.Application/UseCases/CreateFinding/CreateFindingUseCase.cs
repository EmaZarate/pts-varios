using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AzureStorageBlobSettings;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CreateFinding
{
    public class CreateFindingUseCase : ICreateFindingUseCase
    {
        private readonly IWorkflowCore _workflowCore;
        private readonly IAzureStorageRepository _azureStorageRepository;
        private readonly AzureStorageBlobSettings _azureStorageSettings;

        public CreateFindingUseCase(
            IWorkflowCore workflowCore,
            IAzureStorageRepository azureStorageRepository,
            IOptions<AzureStorageBlobSettings> azureStorageSettings)
        {
            _workflowCore = workflowCore;
            _azureStorageRepository = azureStorageRepository;
            _azureStorageSettings = azureStorageSettings.Value;
        }

        public async Task<FindingWorkflowData> Execute(FindingWorkflowData finding, IFormFile[] findingEvidences)
        {
            if(findingEvidences != null)
            {
                await InsertFiles(findingEvidences, finding);
            }
            finding.Flow = "Finding";
            finding.FlowVersion = 1;
            var flowId = await _workflowCore.StartFlow(finding);
            return finding;
        }

        private async Task InsertFiles(IFormFile[] findingEvidences, FindingWorkflowData finding)
        {
            foreach (var fe in findingEvidences)
            {
                var fileToAdd = new Evidence();

                using (var memoryStream = new MemoryStream())
                {
                    await fe.CopyToAsync(memoryStream);
                    fileToAdd.Bytes = memoryStream.ToArray();
                }

                fileToAdd.FileName = fe.FileName;
                fileToAdd.IsInsert = true;
                fileToAdd.IsDelete = false;

                fileToAdd.Url = await _azureStorageRepository.InsertFileAzureStorage(fileToAdd, TypeData.Byte, _azureStorageSettings.ContainerFindingName);
                finding.NewEvidencesUrls.Add(fileToAdd.Url);
            }
        }
    }
}
