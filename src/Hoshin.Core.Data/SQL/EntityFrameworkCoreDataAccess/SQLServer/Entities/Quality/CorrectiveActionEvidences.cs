using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class CorrectiveActionEvidences
    {
        public int CorrectiveActionEvidenceID { get; set; }
        public int CorrectiveActionID { get; set; }
        public CorrectiveActions CorrectiveAction { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
