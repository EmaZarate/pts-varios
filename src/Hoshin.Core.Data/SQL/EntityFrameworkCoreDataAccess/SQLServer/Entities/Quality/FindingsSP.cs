using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class FindingsSP
    {
        public int FindingID { get; set; }
        public string Description { get; set; }
        public int FindingTypeID { get; set; }
        public string FindingTypeName { get; set; }
        public int FindingStateID { get; set; }
        public string FindingStateName { get; set; }
        public string FindingStateCode { get; set; }
        public string FindingStateColor { get; set; }
        public int? SectorTreatmentID { get; set; }
        public string SectorPlantTreatmentSectorName { get; set; }
        public int? PlantTreatmentID { get; set; }
        public string SectorPlantTreatmentPlantName { get; set; }
        public int? SectorPlantTreamtentReferringJobId { get; set; }
        public int? SectorPlantTreamtentReferring2JobId { get; set; }
        public string ResponsibleUserID { get; set; }
        public string ResponsibleUserFullName { get; set; }
        public int? ResponsibleUserJob { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
