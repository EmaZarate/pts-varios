using Hoshin.Core.Domain;
using Hoshin.Core.Domain.Users;
using Hoshin.Quality.Domain.Interfaces;
using System;
using System.Collections.Generic;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;
using Hoshin.Quality.Domain.FindingsState;

namespace Hoshin.Quality.Domain.Finding
{
    public class Finding : IEntity
    {
        public string WorkflowId { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Description { get; set; }
        public string CauseAnalysis { get; set; }
        public string ContainmentAction { get; set; }
        public string FinalComment { get; set; }
        public bool IsInProcessWorkflow { get; set; }
        public string EmitterUserID { get; set; }
        public User EmitterUser { get; set; }
        public string EmitterUserFullname { get { return EmitterUser != null ? EmitterUser.FullName : null; } }
        public string SectorPlantEmitterPlantName { get { return EmitterUser != null && EmitterUser.JobSectorPlant != null && EmitterUser.JobSectorPlant.SectorPlant != null && EmitterUser.JobSectorPlant.SectorPlant.Plant != null ? EmitterUser.JobSectorPlant.SectorPlant.Plant.Name : null; } }
        public string SectorPlantEmitterSectorName { get { return EmitterUser != null && EmitterUser.JobSectorPlant != null && EmitterUser.JobSectorPlant.SectorPlant != null && EmitterUser.JobSectorPlant.SectorPlant.Sector != null ? EmitterUser.JobSectorPlant.SectorPlant.Sector.Name : null; } }
        public string ResponsibleUserID { get; set; }
        public int? ResponsibleUserJob { get { if (ResponsibleUser != null) { return ResponsibleUser.JobID; } else { return null; } } }

        public User ResponsibleUser { get; set; }
        public string ResponsibleUserFullName { get { return ResponsibleUser != null ? ResponsibleUser.FullName : null; } }

        public int? PlantLocationID { get; set; }
        public int? SectorLocationID { get; set; }

        public int? PlantTreatmentID { get; set; }
        public int? SectorTreatmentID { get; set; }
        public SectorPlant SectorPlantTreatment { get; set; }
        public string SectorPlantTreatmentSectorName { get { return SectorPlantTreatment != null && SectorPlantTreatment.Sector != null ? SectorPlantTreatment.Sector.Name : null; } }
        public string SectorPlantTreatmentPlantName { get { return SectorPlantTreatment != null && SectorPlantTreatment.Plant != null ? SectorPlantTreatment.Plant.Name : null; } }
        public string SectorPlantTreatmentName { get { return SectorPlantTreatment != null && SectorPlantTreatment.Plant != null && SectorPlantTreatment.Sector  != null ? SectorPlantTreatment.Sector.Name + " - " + SectorPlantTreatment.Plant.Name: null; } }
        public int? SectorPlantTreamtentReferringJobId { get { return SectorPlantTreatment?.ReferringJob; } }
        public int? SectorPlantTreamtentReferring2JobId { get { return SectorPlantTreatment?.ReferringJob2; } }

        public SectorPlant SectorPlantLocation { get; set; }
        public string SectorPlantLocationSectorName { get { return SectorPlantLocation != null && SectorPlantLocation.Sector != null ? SectorPlantLocation.Sector.Name : null; } }
        public string SectorPlantLocationPlantName { get { return SectorPlantLocation != null && SectorPlantLocation.Plant != null ? SectorPlantLocation.Plant.Name : null; } }

        public int FindingTypeID { get; set; }
        public FindingType.FindingType FindingType { get; set; }
        public string FindingTypeName { get { return FindingType != null ? FindingType.Name : null; } }
        public string FindingTypeCode { get { return FindingType != null ? FindingType.Code : null; } }

        public int FindingStateID { get; set; }
        public FindingsState.FindingsState FindingState { get; set; }
        public string FindingStateName { get { return FindingState != null ? FindingState.Name : null; } }
        public string FindingStateCode { get { return FindingState != null ? FindingState.Code : null; } }
        public string FindingStateColor { get { return FindingState != null ? FindingState.Colour : null; } }
        public bool IsStateExpired { get { return FindingStateName == "Vencido"; } }
        public List<FindingComment> FindingComments { get; set; }
        public List<Hoshin.Quality.Domain.ReassignmentsFindingHistory.ReassignmentsFindingHistory> FindingsReassignmentsHistory { get; set; }

        public List<Evidence.Evidence> FindingsEvidences { get; set; }

        public int? AuditID { get; set; }
        public int? StandardID { get; set; }
        public int? AspectID { get; set; }



        public bool ValidateExpirationDate()
        {
            if (!IsStateExpired && ExpirationDate.Date <= DateTime.Now.Date) return false;
            return true;
        }
    }
}
