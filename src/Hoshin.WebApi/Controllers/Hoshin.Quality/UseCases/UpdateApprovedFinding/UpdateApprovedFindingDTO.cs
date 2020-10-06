using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.UpdateApprovedFinding
{
    public class UpdateApprovedFindingDTO
    {
        public string WorkflowId { get; set; }
        public int FindingID { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Description { get; set; }
        public string EmitterUserID { get; set; }
        public int FindingStateID { get; set; }
        public int FindingTypeID { get; set; }
        public int SectorLocationID { get; set; }
        public int PlantLocationID { get; set; }
        public int SectorTreatmentID { get; set; }
        public int PlantTreatmentID { get; set; }
        public string ResponsibleUserID { get; set; }
        public string CauseAnalysis { get; set; }
        public string ContainmentAction { get; set; }
        public string Comment { get; set; }
        public string EventData { get; set; }
        public IFormFile[] FindingEvidences { get; set; }
        public string[] FileNamesToDelete { get; set; }
    }
}
