using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.CorrectiveAction;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.AspNetCore.Http;
using Hoshin.Quality.Domain.AzureStorageBlobSettings;
using Microsoft.Extensions.Options;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionEvidence.UpdateCorrectiveActionEvidence
{
    public class UpdateCorrectiveActionEvidenceUseCase : IUpdateCorrectiveActionEvidenceUseCase
    {
        private readonly IAzureStorageRepository _azureStorageRepository;
        private readonly ICorrectiveActionEvidenceRepository _correctiveActionEvidenceRepository;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly IMapper _mapper;
        private readonly AzureStorageBlobSettings _azureStorageSettings;

        public UpdateCorrectiveActionEvidenceUseCase(IAzureStorageRepository azureStorageRepository,
            ICorrectiveActionEvidenceRepository correctiveActionEvidenceRepository,
            ICorrectiveActionRepository correctiveActionRepository,
            IMapper mapper,
            IOptions<AzureStorageBlobSettings> azureStorageSettings)
        {
            _azureStorageRepository = azureStorageRepository;
            _correctiveActionEvidenceRepository = correctiveActionEvidenceRepository;
            _correctiveActionRepository = correctiveActionRepository;
            _mapper = mapper;
            _azureStorageSettings = azureStorageSettings.Value;
        }
        public async Task<CorrectiveActionOutput> Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction, IFormFile[] Evidences, List<string> filesToDelete)
        {
            filesToDelete.RemoveAll(string.IsNullOrWhiteSpace);
            try
            {
                if (Evidences != null)
                {
                    await InsertFiles(Evidences, correctiveAction);
                }
                if (filesToDelete != null)
                {
                    await DeleteFiles(filesToDelete, correctiveAction);
                }
                _correctiveActionEvidenceRepository.Update(correctiveAction.CorrectiveActionID, correctiveAction.NewEvidencesUrls, correctiveAction.DeleteEvidencesUrls);
                return _mapper.Map<Domain.CorrectiveAction.CorrectiveAction, CorrectiveActionOutput>(_correctiveActionRepository.GetOne(correctiveAction.CorrectiveActionID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task DeleteFiles(List<string> filesToDelete, Domain.CorrectiveAction.CorrectiveAction correctiveAction)
        {
            foreach (var fe in filesToDelete)
            {
                var fileToDelete = new Evidence();
                fileToDelete.FileName = fe;
                fileToDelete.Url = await _azureStorageRepository.DeleteFileAzureStorage(fileToDelete, _azureStorageSettings.ContainerCorrectiveActionName);
                correctiveAction.DeleteEvidencesUrls.Add(fileToDelete.Url);
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
