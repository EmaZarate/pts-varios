using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CreateFinding
{
    public class CreateFindingDTO
    {
        public string Description { get; set; }
        public int PlantLocationID { get; set; }
        public int SectorLocationID { get; set; }
        public int PlantTreatmentID { get; set; }
        public int SectorTreatmentID { get; set; }
        public int FindingTypeID { get; set; }
        public int FindingStateID { get; set; }
        public string ResponsibleUserID { get; set; }
        public string EmitterUserID { get; set; }
        public IFormFile[] FindingEvidences { get; set; }
    }
}
