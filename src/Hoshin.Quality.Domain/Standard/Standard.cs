using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.Standard
{
    public class Standard
    {

        public Standard()
        {
            this.Aspects = new List<Aspect>();
        }

        public int StandardID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }


        public List<Aspect> Aspects;
    }
}

