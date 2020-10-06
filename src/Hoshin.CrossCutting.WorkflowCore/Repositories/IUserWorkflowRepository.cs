using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IUserWorkflowRepository
    {
        List<string> GetUsersEmailCorrectiveActionApprover();
        List<string> GetUsersEmailResponsibleSGC();
        string GetUserEmailByID(string userId);
        List<string> GetUsersEmailResponsibleFinding();
        List<string> GetUsersEmailColaboratorSB();
        List<string> GetUsersEmailSectorBoss();
        string GetFullName(string id);
    }
}
