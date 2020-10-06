using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class CorrectiveActionFishbone
    {
        public int FishboneID { get; set; }
        public Fishbone Fishbone { get; set; }
        public int CorrectiveActionID { get; set; }
        public CorrectiveActions CorrectiveAction { get; set; }
        public ICollection<CorrectiveActionFishboneCauses> CorrectiveActionFishboneCauses { get; set; }
    }
}
