using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class FindingsStatesHistory
    {
        public int FindingStateHistoryID { get; set; }
        public DateTime Date { get; set; }
        public int FindingStateID { get; set; }
        public int FindingID { get; set; }
        public Findings Finding { get; set; }
        public string CreatedByUserID { get; set; }
    }
}
