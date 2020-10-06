using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.AspectStates
{
    public class AspectStates
    {
        public int AspectStateID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }

        public AspectStates()
        {

        }
        public AspectStates(int id, string name, string colour, bool active)
        {
            AspectStateID = id;
            Name = name;
            Colour = colour;
            Active = active;
        }

        public AspectStates(string name, string colour, bool active)
        {
            Name = name;
            Colour = colour;
            Active = active;
        }
    }
}
