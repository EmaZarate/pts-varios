using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Standard
{
    public class StandardOutput
    {
        public int StandardID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public List<AspectOutput> Aspects;
    }
}
