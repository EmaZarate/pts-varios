using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Core.Domain;
using Hoshin.Core.Domain.Users;
using Hoshin.Quality.Domain;


namespace Hoshin.Quality.Domain.Audit
{
    public class Audit
    {

        public int AuditID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime AuditInitDate { get; set; }
        public int AuditStateID { get; set; }
        public AuditState.AuditState AuditState { get; set; }
        public string AuditStateName { get { return AuditState != null ? AuditState.Name : null; } }
        public string AuditStateColour { get { return AuditState != null ? AuditState.Color : null; } }
        public int AuditTypeID { get; set; }
        public AuditType.AuditType AuditType { get; set; }
        public string AuditTypeName { get { return AuditType != null ? AuditType.Name : null; } }
        public int SectorID { get; set; }
        public int PlantID { get; set; }
        public Hoshin.Core.Domain.SectorPlant SectorPlant { get; set; }
        public string SectorPlantName { get { return SectorPlant != null && SectorPlant.Plant != null && SectorPlant.Sector != null ? SectorPlant.Sector.Name + " - " + SectorPlant.Plant.Name : null; } }
        public string AuditorID { get; set; }
        public User Auditor { get; set; }
        public string ExternalAuditor { get; set; }
        public string AuditorName { get { return AuditorID == null ? ExternalAuditor : Auditor.FullName; } }
        public string AuditTeam { get; set; }
        public DateTime DocumentsAnalysisDate { get; set; }
        public int DocumentAnalysisDuration { get; set; }
        public DateTime AuditInitTime { get; set; }
        public DateTime AuditFinishDate { get; set; }
        public DateTime AuditFinishTime { get; set; }
        public DateTime CloseMeetingDate { get; set; }
        public int CloseMeetingDuration { get; set; }
        public DateTime ReportEmittedDate { get; set; }
        public DateTime CloseDate { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
        public string ApprovePlanComments { get; set; }
        public string ApproveReportComments { get; set; }
        public string WorkflowId { get; set; }
        public List<Int32> AuditStandardsID { get; set; }
        public List<Int32> AuditReschedulingHistoriesID { get; set; }
        public List<AuditStandard> AuditStandards { get; set; }
        public List<AuditReschedulingHistory.AuditReschedulingHistory> AuditReschedulingHistories { get; set; }
    }
}
