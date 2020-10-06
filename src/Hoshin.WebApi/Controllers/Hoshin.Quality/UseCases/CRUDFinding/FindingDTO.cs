using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFinding
{
    public class FindingDTO
    {
        public int FindingID { get; set; }
        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public int AspectID { get; set; }
        public string Description { get; set; }
        public int FindingTypeID { get; set; }
    }
}
