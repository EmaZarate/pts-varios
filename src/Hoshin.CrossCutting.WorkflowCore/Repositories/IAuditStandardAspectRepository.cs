using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IAuditStandardAspectRepository
    {
        List<AuditStandardAspects> AddRange(List<AuditStandardAspects> auditStandardAspect);

    }
}
