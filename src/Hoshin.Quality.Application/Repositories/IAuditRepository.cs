using Hoshin.Quality.Domain.Audit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IAuditRepository
    {
        bool Update(Audit audit);
        Audit Get(int id);
        List<Audit> GetAll();
        List<Audit> GetAllForAuditor(string id);
        List<Audit> GetAllForColaboratorOrSectorBoss(int userJobId);
        bool Delete(int auditId);
        int GetCount();
    }
}
