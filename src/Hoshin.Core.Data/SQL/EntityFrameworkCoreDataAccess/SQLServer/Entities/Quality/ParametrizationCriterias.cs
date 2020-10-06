using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class ParametrizationCriterias
    {
        public int ParametrizationCriteriaID { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public ICollection<ParametrizationsFindingTypes> ParametrizationsFindingTypes { get; set; }
    }
}