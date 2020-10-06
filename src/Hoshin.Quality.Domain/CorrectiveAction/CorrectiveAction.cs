using Hoshin.Core.Domain;
using Hoshin.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.CorrectiveAction
{
    public class CorrectiveAction
    {
        public string WorkflowId { get; set; }
        public int CorrectiveActionID { get; set; }
        public int? FindingID { get; set; }
        public Finding.Finding Finding { get; set; }
        public DateTime CreationDate { get; set; }
        public string EmitterUserID { get; set; }
        public User EmitterUser { get; set; }
        public int? SectorLocationID { get; set; }
        public int? PlantLocationID { get; set; }
        public string LastResponsibleUserID { get; set; }
        public SectorPlant SectorPlantLocation { get; set; }
        public string SectorPlantLocationName { get { return SectorPlantLocation != null && SectorPlantLocation.Plant != null && SectorPlantLocation.Sector != null ? SectorPlantLocation.Sector.Name + " - " + SectorPlantLocation.Plant.Name : null; } }
        public int? SectorTreatmentID { get; set; }
        public int? PlantTreatmentID { get; set; }
        public SectorPlant SectorPlantTreatment { get; set; }
        public int? SectorPlantTreamtentReferringJobId { get { return SectorPlantTreatment?.ReferringJob; } }
        public int? SectorPlantTreamtentReferring2JobId { get { return SectorPlantTreatment?.ReferringJob2; } }
        public string SectorPlantTreatmentName { get { return SectorPlantTreatment != null && SectorPlantTreatment.Plant != null && SectorPlantTreatment.Sector != null ? SectorPlantTreatment.Sector.Name + " - " + SectorPlantTreatment.Plant.Name : null; } }
        public string ResponsibleUserID { get; set; }
        public User ResponisbleUser { get; set; }
        public int? ResponsibleUserJob { get { if (ResponisbleUser != null) { return ResponisbleUser.JobID; } else { return null; } } }
        public string ResponsibleUserFullName { get { return ResponisbleUser != null ? ResponisbleUser.FullName : null; } }
        public string WorkGroup { get; set; }
        public string Description { get; set; }
        public int CorrectiveActionStateID { get; set; }
        public CorrectiveActionState.CorrectiveActionState CorrectiveActionState { get; set; }
        public string CorrectiveActionStateName { get { return CorrectiveActionState != null ? CorrectiveActionState.Name : null; } }
        public string CorrectiveActionStateCode { get { return CorrectiveActionState != null ? CorrectiveActionState.Code : null; } }
        public string CorrectiveActionStateColor { get { return CorrectiveActionState != null ? CorrectiveActionState.Color : null; } }
        public string Impact { get; set; }
        public string ReviewerUserID { get; set; }
        public User ReviewerUser { get; set; }
        public int? ReviewerUserJob { get { if (ReviewerUser != null) { return ReviewerUser.JobID; } else { return null; } } }
        public string ReviewerUserFullName { get { return ReviewerUser != null ? ReviewerUser.FullName : null; } }
        public string RootReason { get; set; }
        public DateTime MaxDateImplementation { get; set; }
        public DateTime EffectiveDateImplementation { get; set; }
        public DateTime MaxDateEfficiencyEvaluation { get; set; }
        public DateTime dateTimeEfficiencyEvaluation { get; set; }
        public DateTime DeadlineDateEvaluation { get; set; }
        public bool isEffective { get; set; }
        public string EvaluationCommentary { get; set; }
        public List<Evidence.Evidence> Evidences { get; set; }
        public List<string> DeleteEvidencesUrls { get; set; }
        public List<string> NewEvidencesUrls { get; set; }
        public DateTime DeadlineDatePlanification { get; set; }
        //public ICollection<CorrectiveActionEvidences> Evidences { get; set; }
        //public ICollection<CorrectiveActionTasks> CorrectiveActionTasks { get; set; }
        public string Flow { get; set; }
        public int FlowVersion { get; set; }
        public ICollection<Domain.CorrectiveActionFishbone.CorrectiveActionFishbone> CorrectiveActionFishbones { get; set; }
        public List<UserCorrectiveAction.UserCorrectiveAction> UserCorrectiveActions { get; set; }
        const int ABIERTA = 1;

        public CorrectiveAction()
        {

        }

        public CorrectiveAction(string description, int relatedFindingId, int plantLocationId, int sectorLocationId, int plantTreatmentId, int sectorTreatmentId, string responsibleUserId, string reviewerUserId)
        {
            this.Description = description;
            this.FindingID = relatedFindingId;
            this.PlantLocationID = plantLocationId;
            this.SectorLocationID = sectorLocationId;
            this.PlantTreatmentID = plantTreatmentId;
            this.SectorTreatmentID = sectorTreatmentId;
            this.ResponsibleUserID = responsibleUserId;
            this.ReviewerUserID = reviewerUserId;
            this.CorrectiveActionStateID = ABIERTA;
            this.CreationDate = DateTime.Now;
            this.NewEvidencesUrls = new List<string>();
            this.DeleteEvidencesUrls = new List<string>();
        }
    }
}
