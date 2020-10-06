using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class JobsSectorsPlants
    {
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public int JobID { get; set; }
        public int JobPlantSupID { get; set; }
        public int JobSupID { get; set; }
        public int JobSectorSupID { get; set; }
        public SectorsPlants SectorPlant { get; set; }
        public Jobs Job { get; set; }
        public ICollection<Users> Users { get; set; }

    }
}
    