using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Helpers;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AzureStorageBlobSettings;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using IFindingRepository = Hoshin.Quality.Application.Repositories.IFindingRepository;

namespace Hoshin.Quality.Application.UseCases.UpdateApprovedFinding
{
    public class UpdateApprovedFindingUseCase : IUpdateApprovedFindingUseCase
    {
        private readonly IWorkflowCore _workflowCore;
        private readonly IAzureStorageRepository _azureStorageRepository;
        private readonly IUserLoggedHelper _userLoggedHelper;
        private readonly Core.Application.Repositories.IUserRepository _userRepository;
        private readonly AzureStorageBlobSettings _azureStorageSettings;
        private IHubContext<FindingsHub> _hub;
        private readonly IFindingRepository _findingRepository;


        public UpdateApprovedFindingUseCase(
            IWorkflowCore workflowCore,
            IAzureStorageRepository azureStorageRepository,
            IUserLoggedHelper userLoggedHelper,
            IUserRepository userRepository,
            IOptions<AzureStorageBlobSettings> azureStorageSettings,
            IHubContext<FindingsHub> hub,
            IFindingRepository findingRepository)
        {
            _workflowCore = workflowCore;
            _azureStorageRepository = azureStorageRepository;
            _userLoggedHelper = userLoggedHelper;
            _userRepository = userRepository;
            _azureStorageSettings = azureStorageSettings.Value;
            _hub = hub;
            _findingRepository = findingRepository;
        }
        public async Task<bool> Execute(FindingWorkflowData finding, IFormFile[] findingEvidences, List<string> filesToDelete)
        {
            if (!_userLoggedHelper.CheckForPermissionsToUpdateReassignOrClose(finding.ResponsibleUserID))
            {
                throw new DontHavePermissionsException("The user doesn´t have permissions to perform this action");
            }
            

            if (findingEvidences != null)
            {
                await InsertFiles(findingEvidences, finding);
            }
            if (filesToDelete != null)
            {
                await DeleteFiles(filesToDelete, finding);
            }
            try
            {
                var findingWorkflow = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, true);
                await _workflowCore.ExecuteEvent("Close", finding.WorkflowId, finding);
                await _hub.Clients.All.SendAsync("transferfindingsdata", findingWorkflow);
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
                fileToDelete.Url = 
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
