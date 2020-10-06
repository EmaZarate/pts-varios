using Hoshin.Quality.Application.UseCases.CorrectiveAction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionEvidence.UpdateCorrectiveActionEvidence
{
    public interface IUpdateCorrectiveActionEvidenceUseCase
    {
        Task<CorrectiveActionOutput> Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction, IFormFile[] Evidences, List<string> filesToDelete);
    }
}
