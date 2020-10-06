using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
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
        public AspectStates AspectState { get; set; }
        public AuditStandard AuditStandard { get; set; }
        public Aspects Aspect { get; set; }
        public ICollection<Findings> Findings { get; set; }

    }
}
