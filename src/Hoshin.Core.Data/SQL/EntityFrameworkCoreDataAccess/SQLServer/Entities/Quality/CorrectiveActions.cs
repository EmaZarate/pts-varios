using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class CorrectiveActions
    {
        public string WorkflowId { get; set; }
        public int CorrectiveActionID { get; set; }
        public int? FindingID { get; set; }
        public Findings Finding { get; set; }
        public DateTime CreationDate { get; set; }
        public string EmitterUserID { get; set; }
        public Users EmitterUser { get; set; }
        public int? SectorLocationID { get; set; }
        public int? PlantLocationID { get; set; }
        public SectorsPlants SectorPlantLocation { get; set; }
        public int? SectorTreatmentID { get; set; }
        public int? PlantTreatmentID { get; set; }
        public SectorsPlants SectorPlantTreatment { get; set; }
        public string ResponsibleUserID { get; set; }
        public Users ResponisbleUser { get; set; }
        public string WorkGroup { get; set; }
        public string Description { get; set; }
        public int CorrectiveActionStateID { get; set; }
        public CorrectiveActionStates CorrectiveActionState { get; set; }
        public string RootReason { get; set; }
        public string ImmediateAction { get; set; }
        public string Impact { get; set; }
        public string ReviewerUserID { get; set; }
        public Users ReviewerUser { get; set; }
        public DateTime MaxDateImplementation { get; set; }
        public DateTime EffectiveDateImplementation { get; set; }
        public DateTime MaxDateEfficiencyEvaluation { get; set; }
        public DateTime dateTimeEfficiencyEvaluation { get; set; }
        public DateTime DeadlineDateEvaluation { get; set; }
        public DateTime DeadlineDatePlanification { get; set; }
        public bool isEffective { get; set; }
        public string EvaluationCommentary { get; set; }
        public ICollection<CorrectiveActionEvidences> Evidences { get; set; }
        public ICollection<CorrectiveActionFishbone> CorrectiveActionFishbones { get; set; }
        public ICollection<UserCorrectiveAction> UserCorrectiveActions { get; set; }
        public ICollection<CorrectiveActionStatesHistory> CorrectiveActionStatesHistory { get; set; }
    }
}
