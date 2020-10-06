using Hoshin.Quality.Domain.Finding;
﻿using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Hoshin.Quality.Domain.Evidence;

namespace Hoshin.Quality.Application.UseCases.ApproveFinding
{
    public interface IApproveFindingUseCase
    {
        Task<bool> Execute(FindingWorkflowData finding, IFormFile[] findingEvidences, List<string> filesToDelete);
        Task<bool> Execute(object finding);
    }
}
