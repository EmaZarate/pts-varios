using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class CorrectiveActionFishboneCauses
    {
        public int CorrectiveActionFishboneCauseID { get; set; }
        public int FishboneID { get; set; }
        public int CorrectiveActionID { get; set; }
        public CorrectiveActionFishbone CorrectiveActionFishbone { get; set; }
        public string Name { get; set; }
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
        public ICollection<CorrectiveActionFishboneCauseWhys> CorrectiveActionFishboneCauseWhys { get; set; }
    }
}
