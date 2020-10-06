using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDStandard
{
    public class StandardDTO
    {

        public StandardDTO()
        {
            this.Aspects = new List<AspectDTO>();
        }

        public int StandardID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public List<AspectDTO> Aspects;
    }
}
