using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class Findings
    {
        public int FindingID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Description { get; set; }
        public string CauseAnalysis { get; set; }
        public string ContainmentAction { get; set; }
        public string FinalComment { get; set; }
        public string EmitterUserID { get; set; }
        public Users EmitterUser { get; set; }
        public string ResponsibleUserID { get; set; }
        public Users ResponsibleUser { get; set; }
        public int? PlantLocationID { get; set; }
        public int? SectorLocationID { get; set; }
        public SectorsPlants SectorPlantLocation { get; set; }
        public int? PlantTreatmentID { get; set; }
        public int? SectorTreatmentID { get; set; }
        public SectorsPlants SectorPlantTreatment { get; set; }
        public int FindingTypeID { get; set; }
        public FindingTypes FindingType { get; set; }
        public CorrectiveActions CorrectiveAction { get; set; }
        public SupplierEvaluations SupplierEvaluation { get; set; }
        public int FindingStateID { get; set; }
        public FindingsStates FindingState { get; set; }
        public ICollection<FindingsReassignmentsHistory> FindingsReassignmentsHistory { get; set; }
        public ICollection<FindingComments> FindingComments { get; set; }
        public ICollection<FindingsStatesHistory> FindingsStatesHistory { get; set; }
        public ICollection<FindingsEvidences> FindingsEvidences { get; set; }
        public string WorkflowId { get; set; }
        public bool IsInProcessWorkflow { get; set; }
        public int? AuditID { get; set; }
        public int? StandardID { get; set; }
        public int? AspectID { get; set; }
        public AuditStandardAspect AuditStandardAspect { get; set; }
        public Findings()
        {
            FindingsReassignmentsHistory = new List<FindingsReassignmentsHistory>();
            FindingComments = new List<FindingComments>();
            FindingsStatesHistory = new List<FindingsStatesHistory>();
            FindingsEvidences = new List<FindingsEvidences>();
        }
    }
}
