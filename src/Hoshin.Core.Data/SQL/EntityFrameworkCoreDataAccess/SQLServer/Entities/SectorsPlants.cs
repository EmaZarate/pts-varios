using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class SectorsPlants
    {
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public int ReferringJob { get; set; }
        public int ReferringJob2 { get; set; }
        public Plants Plant { get; set; }
        public Sectors Sector { get; set; }
        public ICollection<JobsSectorsPlants> JobsSectorsPlants { get; set; }
        public ICollection<Findings> FindingLocation { get; set; }
        public ICollection<Findings> FindingTreatment { get; set; }
        public ICollection<Audits> Audits { get; set; }
    }
}
