using Hoshin.Quality.Domain.FindingType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDPlants
{
    public class PlantDTO
    {
        public int PlantID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
    }
}