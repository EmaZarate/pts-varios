using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain
{
    public class AuditStandard
    {
        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public Standard.Standard Standard { get; set; }
        public Audit.Audit Audit { get; set; }
        public List<AuditStandardAspect> AuditStandardAspects { get; set; }
    }
}
