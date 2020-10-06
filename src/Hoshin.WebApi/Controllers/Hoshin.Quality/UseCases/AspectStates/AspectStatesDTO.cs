using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.AspectStates
{
    public class AspectStatesDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }
    }
}
