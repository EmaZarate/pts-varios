using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Repositories
{
    public interface IAuditRepository
    {
        AuditWorkflowData Add(AuditWorkflowData audit);
        bool ApproveOrRejectAuditPlan(AuditWorkflowData audit);
        bool ApproveOrRejectReportAudit(AuditWorkflowData audit);
        string GetAuditorEmail(string id);
        bool PlanAudit(AuditWorkflowData audit, List<AuditStandardAspects> auditStandardAspects);
        AuditWorkflowData GetOneByWorkflowId(string id);
        bool EmmitReport(AuditWorkflowData audit);
    }
}
