using System;
using System.Collections.Generic;
using Hoshin.Quality.Application.UseCases.FindingType;
using Hoshin.Quality.Domain.Evidence;
using Hoshin.Quality.Domain.FindingsState;


namespace Hoshin.Quality.Application.UseCases.Finding
{
    public class FindingOutput
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Description { get; set; }
        public string CauseAnalysis { get; set; }
        public string ContainmentAction { get; set; }
        public string EmitterUserID { get; set; }
        public string PlantEmitterID { get; set; }
        public string SectorEmitterID { get; set; }
        //public string EmitterUserFullname { get; set; }
        public string WorkflowId { get; set; }
        public string ResponsibleUserID { get; set; }
        public string ResponsibleUserFullName { get; set; }
        public int ResponsibleUserJob { get; set; }
        public int PlantLocationID { get; set; }
        public int SectorLocationID { get; set; }
        public int PlantTreatmentID { get; set; }
        public int SectorTreatmentID { get; set; }
        public string SectorPlantTreatmentSectorName { get; set; }
        public string SectorPlantTreatmentPlantName { get; set; }
        public string SectorPlantTreatmentName { get; set; }
        public int SectorPlantTreamtentReferringJobId { get; set; }
        public int SectorPlantTreamtentReferring2JobId { get; set; }
        public string SectorPlantLocationSectorName { get; set; }
        public string SectorPlantLocationPlantName { get; set; }
        public string SectorPlantEmitterSectorName { get; set; }
        public string SectorPlantEmitterPlantName { get; set; }
        public int FindingTypeID { get; set; }
        public string FindingTypeName { get; set; }
        public string FindingTypeCode { get; set; }

        public int FindingStateID { get; set; }
        public string FindingStateName { get; set; }
        public string FindingStateCode { get; set; }
        public string FindingStateColor { get; set; }
        public bool IsInProcessWorkflow { get; set; }

        public List<Domain.Finding.FindingComment> FindingComments { get; set; }

        public string FindingsReassignmentsHistoryState { get; set; }
        public FindingsState FindingState { get; set; }
        public string FinalComment { get; set; }
        public List<Evidence> FindingsEvidences { get; set; }
        public FindingTypeOutput FindingType { get; set; }

        public int? AuditID { get; set; }
        public int? StandardID { get; set; }
        public int? AspectID { get; set; }

        public FindingOutput()
        {

        }
    }
}
