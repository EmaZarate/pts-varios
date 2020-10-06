using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class UserCorrectiveAction
    {
        public string UserID { get; set; }
        public int CorrectiveActionID { get; set; }
        public Users Users { get; set; }
        public CorrectiveActions CorrectiveActions { get; set; }

    }
}