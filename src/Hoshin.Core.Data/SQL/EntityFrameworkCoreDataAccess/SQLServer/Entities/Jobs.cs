using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class Jobs
    {
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public ICollection<JobsSectorsPlants> JobsSectorsPlants { get; set; }
    }
}
