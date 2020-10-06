using Hoshin.Core.Application.UseCases.User.GetAllUser;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates;
using Hoshin.Quality.Application.UseCases.Finding;
using Hoshin.Quality.Domain.CorrectiveActionEvidence;
using Hoshin.Quality.Domain.Evidence;
using Hoshin.Quality.Domain.CorrectiveActionFishbone;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction
{
    public class CorrectiveActionOutput
    {
        public string WorkflowId { get; set; }
        public int CorrectiveActionID { get; set; }
        public int? FindingID { get; set; }
        public FindingOutput Finding { get; set; }
        public DateTime CreationDate { get; set; }
        public string EmitterUserID { get; set; }
        public UserOutput EmitterUser { get; set; }
        public int? SectorLocationID { get; set; }
        public int? PlantLocationID { get; set; }
        public Core.Domain.SectorPlant SectorPlantLocation { get; set; }
        public string SectorPlantLocationName { get; set; }
        public int? SectorTreatmentID { get; set; }
        public int? PlantTreatmentID { get; set; }
        public Core.Domain.SectorPlant SectorPlantTreatment { get; set; }
        public int? SectorPlantTreamtentReferringJobId { get; set; }
        public int? SectorPlantTreamtentReferring2JobId { get; set; }
        public string SectorPlantTreatmentName { get; set; }
        public string ResponsibleUserID { get; set; }
        public UserOutput ResponisbleUser { get; set; }
        public int? ResponsibleUserJob { get; set; }
        public string ResponsibleUserFullName { get; set; }
        public string WorkGroup { get; set; }
        public string Description { get; set; }
        public int CorrectiveActionStateID { get; set; }
        public CorrectiveActionStateOutput CorrectiveActionState { get; set; }
        public string CorrectiveActionStateName { get; set; }
        public string CorrectiveActionStateCode { get; set; }
        public string CorrectiveActionStateColor { get; set; }
        public string Impact { get; set; }
        public string RootReason { get; set; }
        public string ReviewerUserID { get; set; }
        public UserOutput ReviewerUser { get; set; }
        public int? ReviewerUserJob { get; set; }
        public string ReviewerUserFullName { get; set; }
        public DateTime MaxDateImplementation { get; set; }
        public DateTime EffectiveDateImplementation { get; set; }
        public DateTime MaxDateEfficiencyEvaluation { get; set; }
        public DateTime dateTimeEfficiencyEvaluation { get; set; }
        public DateTime DeadlineDateEvaluation { get; set; }
        public DateTime DeadlineDatePlanification { get; set; }
        public bool isEffective { get; set; }
        public string EvaluationCommentary { get; set; }
        public ICollection<Domain.CorrectiveActionFishbone.CorrectiveActionFishbone> CorrectiveActionFishbone { get; set; }
        public List<Evidence> Evidences { get; set; }
        public List<Domain.UserCorrectiveAction.UserCorrectiveAction> UserCorrectiveActions { get; set; }
    }
}
