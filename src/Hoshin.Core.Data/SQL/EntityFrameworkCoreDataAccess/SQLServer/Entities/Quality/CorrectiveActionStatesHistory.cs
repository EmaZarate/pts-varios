using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class CorrectiveActionStatesHistory
    {
        public int CorrectiveActionStatesHistoryID { get; set; }
        public DateTime Date { get; set; }
        public int CorrectiveActionStateID { get; set; }
        public int CorrectiveActionID { get; set; }
        public CorrectiveActions CorrectiveAction { get; set; }
        public string CreatedByUserID { get; set; }
    }
}
