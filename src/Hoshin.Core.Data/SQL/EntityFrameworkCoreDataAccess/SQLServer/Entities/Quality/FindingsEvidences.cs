using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class FindingsEvidences
    {
        public int FindingEvidenceID { get; set; }
        public int FindingID { get; set; }
        public Findings Finding { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
