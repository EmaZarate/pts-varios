using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class Plants
    {
        public int PlantID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public ICollection<SectorsPlants> SectorsPlants { get; set; }
    }
}
