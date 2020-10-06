using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.CorrectiveActionFishbone
{
    public class CorrectiveActionFishboneWhy
    {
        public int CorrectiveActionId { get; set; }
        public int FishboneId { get; set; }
        public int CauseId { get; set; }
        public int WhyId { get; set; }
        public string Description { get; set; }
        public string SubChildren { get; set; }
        public string Index { get; set; }
    }
}
