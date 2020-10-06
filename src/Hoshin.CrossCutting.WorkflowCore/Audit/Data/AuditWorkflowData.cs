using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Audit.Data
{
        public class AuditWorkflowData : IDataWorkflow
    {
        public int AuditID { get; set; }
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public string SectorName { get; set; }
        public string AuditorID { get; set; }
        public string AuditorFullName { get; set; }
        public string ExternalAuditor { get; set; }
        public int AuditStateID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime AuditInitDate { get; set; }
        public DateTime AuditInitTime { get; set; }
        public DateTime AuditFinishDate { get; set; }
        public DateTime AuditFinishTime { get; set; }
        public int AuditTypeID { get; set; }
        public string AuditTypeName { get; set; }
        public List<int> AuditStandard { get; set; }
        public List<AuditStandards> AuditStandards { get; set; }
        public string AuditTeam { get; set; }
        public List<AuditStandardAspects> AuditStandardAspects { get; set; }
        public DateTime DocumentsAnalysisDate { get; set; }
        public int DocumentAnalysisDuration { get; set; }
        public DateTime ReportEmittedDate { get; set; }
        public DateTime CloseMeetingDate { get; set; }
        public int CloseMeetingDuration { get; set; }
        public DateTime CloseDate { get; set; }
        public string Commentary { get; set; }
        public string ApprovePlanComments { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        public string WorkflowId { get; set; }
        public string WorkflowData { get; set; }
        public string Flow { get; set; }
        public int FlowVersion { get; set; }
        public string EventData { get; set; }
        public string EventName { get; set; }
        public string State { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
        public string ApproveReportComments { get; set; }

        public AuditWorkflowData() { }
    }

    public class AuditStandards
    {
        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public Standards Standard { get; set; }
        public string StandardName { get { return Standard != null ? Standard.Name : null; } }
    }

    public class Standards
    {
        public int StandardID { get; set; }
        public string Name { get; set; }
    }

    public class AuditStandardAspects
    {
        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public int AspectID { get; set; }
        public int AspectStateID { get; set; }
    }
}
