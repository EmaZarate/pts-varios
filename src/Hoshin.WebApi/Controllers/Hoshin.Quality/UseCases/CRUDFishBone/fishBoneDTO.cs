using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFishBone
{
    public class FishBoneDTO
    {
        public int FishboneID { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
