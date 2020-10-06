using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ReassignFinding
{
    public class ReassignmentsFindingHistoryDTO
    {
        public string WorkflowId { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ReassignedUserID { get; set; }
        public int PlantTreatmentID { get; set; }
        public int SectorTreatmentID { get; set; }
        public string LastResponsibleUserID { get; set; }
        public int FindingID { get; set; }
        public string CreatedByUserID { get; set; }
        public string EventData { get; set; }
        public string RejectComment { get; set; }
        public string State { get; set; }
    }
}
