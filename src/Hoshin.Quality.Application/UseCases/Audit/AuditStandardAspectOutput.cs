using Hoshin.Quality.Application.UseCases.Finding;
using Hoshin.Quality.Application.UseCases.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Audit
{
    public class AuditStandardAspectOutput
    {

        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public int AspectID { get; set; }
        public int AspectStateID { get; set; }
        public bool NoAudited { get; set; }
        public bool WithoutFindings { get; set; }
        public string Description { get; set; }
        public AspectOutput Aspect { get; set; }
        public ICollection<FindingOutput> Findings { get; set; }

    }
}
