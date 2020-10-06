using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Finding.Data
{
    public class FindingWorkflowData : IDataWorkflow
    {
        public int FindingID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ContainmentAction { get; set; }
        public string CauseAnalysis { get; set; }
        public string Comment { get; set; }
        public string FinalComment { get; set; }
        public int PlantLocationID { get; set; }
        public int SectorLocationID { get; set; }
        public int PlantTreatmentID { get; set; }
        public int SectorTreatmentID { get; set; }
        public int FindingTypeID { get; set; }
        public string LastResponsibleUserID { get; set; }
        public int FindingStateID { get; set; }
        public string ResponsibleUserID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EmitterUserID { get; set; }
        public string ReassignedUserID { get; set; }
        public string RejectComment { get; set; }
        public List<string> EvidencesUrls { get; set; }
        public List<string> NewEvidencesUrls { get; set; }
        public List<string> DeleteEvidencesUrls { get; set; }
        public string WorkflowId { get; set; }
        public string WorkflowData { get; set; }
        public string Flow { get; set; }
        public int FlowVersion { get; set; }
        public string EventData { get; set; }
        //public object EventData { get; set; }
        public string EventName { get; set; }
        public List<string> EmailAddresses { get; set; }
        const int EN_ESPERA_DE_APROBACION = 10;
        public string State { get; set; }

        public bool IsInProcessWorkflow { get; set; }

        public string FindingStateCode { get; set; }
        public string FindingStateName { get; set; }
        public string FindingStateColor { get; set; }
        public string FindingTypeName { get; set; }
        public string ResponsibleUserFullName { get; set; }
        public string SectorPlantTreatmentSectorName { get; set; }
        public string SectorPlantTreatmentPlantName { get; set; }
        public string SectorPlantTreatmentName { get; set; }
        public string SectorPlantLocationSectorName { get; set; }

        public List<FindingComments> FindingComments { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public string ReviewerUserID { get; set; }
        public FindingWorkflowData() { }

        public FindingWorkflowData(string description, int plantLocationId, int sectorLocationId, int plantTreatmentId, int sectorTreatmentId, int findingTypeId, string emitterUserId, string responsibleUserId)
        {
            this.Description = description;
            this.PlantLocationID = plantLocationId;
            this.SectorLocationID = sectorLocationId;
            this.PlantTreatmentID = plantTreatmentId;
            this.SectorTreatmentID = sectorTreatmentId;
            this.ResponsibleUserID = responsibleUserId;
            this.FindingTypeID = findingTypeId;
            this.EmitterUserID = emitterUserId;
            this.FindingStateID = EN_ESPERA_DE_APROBACION;
            this.CreatedDate = DateTime.Now;
            this.EmailAddresses = new List<string>();
            this.EvidencesUrls = new List<string>();
            this.NewEvidencesUrls = new List<string>();
            this.DeleteEvidencesUrls = new List<string>();
        }
    }

    public class FindingComments
    {
        public int FindingCommentID { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int FindingID { get; set; }
        public string CreatedByUserID { get; set; }
    }

}
