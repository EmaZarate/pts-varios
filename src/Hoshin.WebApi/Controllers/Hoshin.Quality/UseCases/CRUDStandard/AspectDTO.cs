using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDStandard
{
    public class AspectDTO
    {
        public int AspectID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
    }
}
