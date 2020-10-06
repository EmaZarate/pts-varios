using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.AuditStandardAspect
{
    public class AuditStandardAspectFindingDTO
    {
        public int AspectID { get; set; }
        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public int FindingID { get; set; }
        public int FindingTypeID { get; set; }
        public string Description { get; set; }
    }
}
