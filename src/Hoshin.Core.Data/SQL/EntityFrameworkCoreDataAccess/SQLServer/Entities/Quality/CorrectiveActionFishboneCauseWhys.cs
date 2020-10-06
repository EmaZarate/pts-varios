using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class CorrectiveActionFishboneCauseWhys
    {
        public int CorrectiveActionFishboneCauseWhyID { get; set; }
        public int CorrectiveActionFishboneCauseID { get; set; }
        public CorrectiveActionFishboneCauses CorrectiveActionFishboneCause { get; set; }
        public string Description { get; set; }
        public string SubChildren { get; set; }
        public int Index { get; set; }
    }
}
