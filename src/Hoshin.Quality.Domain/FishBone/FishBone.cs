using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.FishBone
{
    public class FishBone
    {
        public int FishboneID { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public FishBone(string name, string color, bool active, int id)
        {
            this.FishboneID = id;
            this.Active = active;
            this.Color = color;
            this.Name = name;
            
        }

        public FishBone(string name, string color, bool active)
        {
            this.Active = active;
            this.Color = color;
            this.Name = name;
        }

    }

}
