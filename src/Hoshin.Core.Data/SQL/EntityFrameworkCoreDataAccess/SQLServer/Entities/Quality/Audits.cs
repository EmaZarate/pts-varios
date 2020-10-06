﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class Audits
    {
        public int AuditID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? AuditInitDate { get; set; }
        public int AuditStateID { get; set; }
        public AuditStates AuditState { get; set; }
        public int AuditTypeID { get; set; }
        public AuditsTypes AuditType { get; set; }
        public int SectorID { get; set; }
        public int PlantID { get; set; }
        public SectorsPlants SectorPlant { get; set; }
        public string AuditorID { get; set; }
        public Users Auditor { get; set; }
        public string ExternalAuditor { get; set; }
        public string AuditTeam { get; set; }
        public DateTime DocumentsAnalysisDate { get; set; }
        public int? DocumentAnalysisDuration { get; set; }
        public DateTime? AuditInitTime { get; set; }
        public DateTime? AuditFinishDate { get; set; }
        public DateTime? AuditFinishTime { get; set; }
        public DateTime? CloseMeetingDate { get; set; }
        public int? CloseMeetingDuration { get; set; }
        public DateTime? ReportEmittedDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
        public string ApprovePlanComments { get; set; }
        public string ApproveReportComments { get; set; }
        public ICollection<AuditStandard> AuditStandards { get; set; }
        public ICollection<AuditReschedulingHistory> AuditReschedulingHistories{ get; set; }

        public string WorkflowId { get; set; }

    }
}
