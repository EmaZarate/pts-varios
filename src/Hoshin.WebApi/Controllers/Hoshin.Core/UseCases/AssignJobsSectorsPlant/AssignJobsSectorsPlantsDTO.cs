using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.AssignJobsSectorsPlant
{
    public class AssignJobsSectorsPlantsDTO
    {
        public int Id { get; set; }
        [JsonProperty("children")]
        public List<AssignSectorsPlantDTO> Sectors { get; set; }

    }

    public class AssignSectorsPlantDTO
    {
        public int Id { get; set; }
        [JsonProperty("children")]
        public List<JobsSectorDTO> Jobs { get; set; }
    }

    public class JobsSectorDTO
    {
        public int Id { get; set; } 
    }
}
