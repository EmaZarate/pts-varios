using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class SupplierEvaluations
    {
        public int SupplierEvaluationID { get; set; }
        public int FindingID { get; set; }
        public Findings Finding { get; set; }
    }
}
