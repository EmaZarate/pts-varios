using Hoshin.Quality.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDAudit
{
    public class AuditDTO
    {
        public int AuditID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime AuditInitDate { get; set; }
        public int AuditStateID { get; set; }
        public string AuditStateCode { get; set; }
        public int AuditTypeID { get; set; }
        public int SectorID { get; set; }
        public int PlantID { get; set; }
        public string AuditorID { get; set; }
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
        public List<int> AuditStandard { get; set; }
        public List<AuditStandard> AuditStandards { get; set;}
        public List<AuditStandardAspect> AuditStandardAspect { get; set; }
    }

    public class AuditStandardAspect
    {
        public int AuditID;
        public int StandardID;
        public int AspectID;
        public int AspectStateID;
    }
}
