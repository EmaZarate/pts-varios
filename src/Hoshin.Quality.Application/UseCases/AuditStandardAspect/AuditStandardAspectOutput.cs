using Hoshin.Quality.Domain.Aspect;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditStandardAspect
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
        public Domain.Aspect.Aspect Aspect { get; set; }
        public List<Domain.Finding.Finding> Findings { get; set; }
    }
}
