using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class ParametrizationsFindingTypes
    {
        public int FindingTypeID { get; set; }
        public int ParametrizationCriteriaID { get; set; }
        public FindingTypes FindingType { get; set; }
        public ParametrizationCriterias ParametrizationCriteria { get; set; }
        public string Value { get; set; }
    }
}