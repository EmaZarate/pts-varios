using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.CorrectiveActionFishbone
{
    public class CorrectiveActionFishbone
    {
        public int CorrectiveActionID { get; set; }
        public int FishboneID { get; set; }
        public List<CorrectiveActionFishboneCause> Causes { get; set; }

    }
}
