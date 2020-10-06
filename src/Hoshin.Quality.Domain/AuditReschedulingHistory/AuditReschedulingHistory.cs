using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.AuditReschedulingHistory
{
    public class AuditReschedulingHistory
    {
        public int AuditReschedulingHistoryID { get; set; }
        public int AuditID { get; set; }
        public DateTime DateRescheduling { get; set; }
    }
}
