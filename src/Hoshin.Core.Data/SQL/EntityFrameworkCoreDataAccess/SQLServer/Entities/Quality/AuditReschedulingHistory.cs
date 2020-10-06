using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class AuditReschedulingHistory
    {
        public int AuditReschedulingHistoryID { get; set; }
        public int AuditID { get; set; }
        public DateTime DateRescheduling { get; set; }
    }
}
