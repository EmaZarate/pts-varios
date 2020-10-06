using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class FindingTypes
    {
        public int FindingTypeID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }

        public ICollection<ParametrizationsFindingTypes> ParametrizationsFindingTypes { get; set; }
        public ICollection<Findings> Findings { get; set; }
    }
}