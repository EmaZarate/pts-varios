using Hoshin.Quality.Domain;
using Hoshin.Quality.Domain.Finding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IAuditStandardAspectRepository
    {
        List<AuditStandardAspect> GetAllforAudit(int id);
        bool AddFinding(Finding finding);
        AuditStandardAspect Get(int auditId, int standardId, int aspectId);
        void SetPendingState(AuditStandardAspect auditStandardAspect);
        void SetWithoutFinding(AuditStandardAspect auditStandardAspect);
        void SetNoAudited(AuditStandardAspect auditStandardAspect);
    }
}
