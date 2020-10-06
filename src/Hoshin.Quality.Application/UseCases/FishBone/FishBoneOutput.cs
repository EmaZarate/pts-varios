using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FishBone
{
   public class FishBoneOutput
    {
        public int FishboneID { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public FishBoneOutput(int id, string name, string color, bool active)
        {
            this.FishboneID = id;
            this.Name = name;
            this.Color = color;
            this.Active = active;
        }
    }
   
}
