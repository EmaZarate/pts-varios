using Hoshin.Quality.Domain.FindingType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDSectors
{
    public class SectorDTO
    {
        public int SectorId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}