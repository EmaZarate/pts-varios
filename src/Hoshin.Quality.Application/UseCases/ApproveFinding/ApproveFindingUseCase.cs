using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AzureStorageBlobSettings;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Hoshin.Quality.Application.UseCases.ApproveFinding
{    
    public class ApproveFindingUseCase : IApproveFindingUseCase
    {
        private readonly IWorkflowCore _workflowCore;
        private readonly IAzureStorageRepository _azureStorageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFindingRepository _findingRepository;
        private IHubContext<FindingsHub> _hub;
        private readonly AzureStorageBlobSettings _azureStorageSettings;

        public ApproveFindingUseCase(
            IWorkflowCore workflowCore,
            IAzureStorageRepository azureStorageRepository,
            IHttpContextAccessor httpContextAccessor,
            IFindingRepository findingRepository,
            IHubContext<FindingsHub> hub,
            IOptions<AzureStorageBlobSettings> azureStorageSettings)
        {
            _workflowCore = workflowCore;
            _azureStorageRepository = azureStorageRepository;
            _httpContextAccessor = httpContextAccessor;
            _findingRepository = findingRepository;
            _hub = hub;
            _azureStorageSettings = azureStorageSettings.Value;
        }

        public async Task<bool> Execute(FindingWorkflowData finding, IFormFile[] findingEvidences, List<string> filesToDelete)
        {
            if (findingEvidences != null)
            {
                await InsertFiles(findingEvidences, finding);
            }
            if(filesToDelete != null)
            {
                await DeleteFiles(filesToDelete, finding);
            }


            try
            {                    
                var findingWorkflow = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, true);            
                finding.EmitterUserID = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
                await _workflowCore.ExecuteEvent("Approve", finding.WorkflowId, finding);
                await _hub.Clients.All.SendAsync("transferfindingsdata", findingWorkflow);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Execute(object finding)
        {
            //_findingRepository.Update(finding);
            try
            {
                await _workflowCore.ExecuteEvent("Approve", "68323d2e-563c-4766-be8a-9fa3a8e9ef50", finding);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task DeleteFiles(List<string> filesToDelete, FindingWorkflowData finding)
        {
            foreach (var fe in filesToDelete)
            {
                var fileToDelete = new Evidence();
                fileToDelete.FileName = fe;
                fileToDelete.Url = await _azureStorageRepository.DeleteFileAzureStorage(fileToDelete, _azureStorageSettings.ContainerFindingName);
                finding.DeleteEvidencesUrls.Add(fileToDelete.Url);
            }
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
