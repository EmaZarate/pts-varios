using Hoshin.Quality.Domain.Finding;
using System.Collections.Generic;
﻿using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IFindingRepository
    {
        List<Finding> GetAll();
        List<Finding> GetAllFromUser(string userId, int plantId, int sectorId);
        List<Finding> GetAllFromSectorPlant(int plantId, int sectorId);
        List<Finding> GetAllApprovedInProgress();
        int GetCount();
        Finding Get(int id);
        FindingWorkflowData Update(FindingWorkflowData finding);
        bool Update(Finding finding);
        bool Delete(int id);
        bool UpdateExpirationDate(Finding finding);
        Finding GetWithoutIncludes(int id);
        FindingWorkflowData UpdateIsInProcessWorkflow(int findingID, bool isInProcessWorkflow);
    }
}
