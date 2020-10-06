using Hoshin.Core.Application.UseCases.User.GetAllUser;
using Hoshin.Quality.Application.UseCases.AuditState;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes;
using Hoshin.Quality.Domain.AuditReschedulingHistory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Audit
{
    public class AuditOutput
    {
        public int AuditID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime AuditInitDate { get; set; }
        public int AuditStateID { get; set; }
        public AuditStateOutput AuditState { get; set; }
        public string AuditStateName { get; set; }
        public string AuditStateColour { get; set; }
        public int AuditTypeID { get; set; }
        public AuditTypeOutput AuditType { get; set; }
        public string AuditTypeName { get; set; }
        public int SectorID { get; set; }
        public int PlantID { get; set; }
        public Hoshin.Core.Domain.SectorPlant SectorPlant { get; set; }
        public string SectorPlantName { get; set; }
        public string AuditorID { get; set; }
        public UserOutput Auditor { get; set; }
        public string AuditorName { get; set; }
        public string ExternalAuditor { get; set; }
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
        public List<AuditStandardOutput> AuditStandards { get; set; }
        public List<AuditReschedulingHistory> AuditReschedulingHistories { get; set; }

    }
}
