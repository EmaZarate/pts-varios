using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Core.Domain.Sector;

namespace Hoshin.Core.Domain.Plant
{
    public class Plant
    {
        public Plant()
        {

        }
        public Plant(string name, string country, bool active)
        {
            Name = name;
            Country = country;
            Active = active;
        }
        public Plant(int id, string name, string country, bool active)
        {
            PlantID = id;
            Name = name;
            Country = country;
            Active = active;
        }
        public int PlantID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public List<Hoshin.Core.Domain.Sector.Sector> Sectors { get; set; }
    }
}
