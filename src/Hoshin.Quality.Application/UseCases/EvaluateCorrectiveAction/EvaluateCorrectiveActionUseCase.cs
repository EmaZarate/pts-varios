using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AzureStorageBlobSettings;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Hoshin.Quality.Application.UseCases.EvaluateCorrectiveAction
{
    public class EvaluateCorrectiveActionUseCase : IEvaluateCorrectiveActionUseCase
    {
        private readonly IAzureStorageRepository _azureStorageRepository;
        private readonly IWorkflowCore _workflowCore;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly AzureStorageBlobSettings _azureStorageSettings;

        public EvaluateCorrectiveActionUseCase(IAzureStorageRepository azureStorageRepository, 
            IWorkflowCore workflowCore,
            ICorrectiveActionRepository correctiveActionRepository,
            IOptions<AzureStorageBlobSettings> azureStorageSettings
            )
        {
            _azureStorageRepository = azureStorageRepository;
            _workflowCore = workflowCore;
            _correctiveActionRepository = correctiveActionRepository;
            _azureStorageSettings = azureStorageSettings.Value;
        }
        public async Task Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction, IFormFile[] Evidences)
        {
            try
            {
                if (Evidences != null)
                {
                    await InsertFiles(Evidences, correctiveAction);
                }

                var workflowId = _correctiveActionRepository.GetWorkflowId(correctiveAction.CorrectiveActionID);

                var correctiveActionWorkflow = new CorrectiveActionWorkflowData();

                correctiveActionWorkflow.WorkflowId = workflowId;
                correctiveActionWorkflow.isEffective = correctiveAction.isEffective;
                correctiveActionWorkflow.EvidencesUrl = correctiveAction.NewEvidencesUrls;
                correctiveActionWorkflow.EvaluationCommentary = correctiveAction.EvaluationCommentary;

                await _workflowCore.ExecuteEvent("ReviewedCorrectiveAction", workflowId, correctiveActionWorkflow);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        private async Task InsertFiles(IFormFile[] findingEvidences, Domain.CorrectiveAction.CorrectiveAction correctiveAction)
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

                fileToAdd.Url = await _azureStorageRepository.InsertFileAzureStorage(fileToAdd, TypeData.Byte, _azureStorageSettings.ContainerCorrectiveActionName);
                correctiveAction.NewEvidencesUrls.Add(fileToAdd.Url);
            }
        }
    }
}
