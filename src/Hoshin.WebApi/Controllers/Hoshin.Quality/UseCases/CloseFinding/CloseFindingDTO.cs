using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CloseFinding
{
    public class CloseFindingDTO
    {
        public string WorkflowId { get; set; }
        //[JsonProperty("id")]
        public int FindingID { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Description { get; set; } //Not Sure
        public string EmitterUserID { get; set; }
        public int FindingStateID { get; set; }
        public int FindingTypeID { get; set; }
        public int SectorLocationID { get; set; }
        public int PlantLocationID { get; set; }
        public int SectorTreatmentID { get; set; }
        public int PlantTreatmentID { get; set; }
        public string ResponsibleUserID { get; set; }        
        public string FinalComment { get; set; }
        public string EventData { get; set; }
        public string State { get; set; }
    }
}
