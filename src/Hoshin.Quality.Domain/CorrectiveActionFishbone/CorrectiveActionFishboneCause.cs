using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.CorrectiveActionFishbone
{
    public class CorrectiveActionFishboneCause
    {
        public int CorrectiveActionID { get; set; }
        public int FishboneID { get; set; }
        public int CauseID { get; set; }
        public decimal X1 { get; set; }
        public decimal X2 { get; set; }
        public decimal Y1 { get; set; }
        public decimal Y2 { get; set; }
        public string Name { get; set; }
        public List<CorrectiveActionFishboneWhy> Whys { get; set; }

    }
}
