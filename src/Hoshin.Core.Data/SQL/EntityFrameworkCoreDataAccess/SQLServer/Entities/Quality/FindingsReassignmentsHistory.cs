using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
   public class FindingsReassignmentsHistory
    {
        public int FindingReassignmentHistoryID { get; set; }
        public DateTime Date { get; set; }
        public string ReassignedUserID { get; set; }
        public int FindingID { get; set; }
        public Findings Finding { get; set; }
        public string CreatedByUserID { get; set; }
        public string State { get; set; }
        public string CauseOfReject { get; set; }
    }
}
