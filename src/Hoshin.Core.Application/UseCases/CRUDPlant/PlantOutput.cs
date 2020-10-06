using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDPlant
{
    public class PlantOutput
    {
        public int PlantID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public List<Hoshin.Core.Domain.Sector.Sector> Sectors { get; set; }
    }
}
