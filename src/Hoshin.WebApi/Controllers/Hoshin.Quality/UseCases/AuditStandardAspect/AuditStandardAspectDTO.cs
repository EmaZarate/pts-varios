using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.AuditStandardAspect
{
    public class AuditStandardAspectDTO
    {
        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public int AspectID { get; set; }
        public int AspectStateID { get; set; }
        public bool NoAudited { get; set; }
        public bool WithoutFindings { get; set; }
        public string Description { get; set; }
    }
}
