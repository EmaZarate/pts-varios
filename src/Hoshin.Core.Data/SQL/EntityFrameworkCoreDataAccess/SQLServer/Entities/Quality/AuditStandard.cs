using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class AuditStandard
    {
        public int AuditID { get; set; }
        public int StandardID { get; set; }
        public Audits Audit { get; set; }
        public Standards Standard { get; set; }
        public ICollection<AuditStandardAspect> AuditStandardAspects { get; set; }
    }
}
