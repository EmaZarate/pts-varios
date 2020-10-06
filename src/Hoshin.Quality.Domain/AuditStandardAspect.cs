using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Domain.Aspect;

namespace Hoshin.Quality.Domain
{
    public class AuditStandardAspect
    {
        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public int AspectID { get; set; }
        public int AspectStateID { get; set; }
        public bool NoAudited { get; set; }
        public bool WithoutFindings { get; set; }
        public string Description { get; set; }
        public Aspect.Aspect Aspect { get; set; }
        public ICollection<Finding.Finding> Findings { get; set; }

    }
}
