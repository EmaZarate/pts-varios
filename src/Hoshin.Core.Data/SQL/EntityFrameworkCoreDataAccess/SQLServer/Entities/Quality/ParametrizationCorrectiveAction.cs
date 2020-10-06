using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class ParametrizationCorrectiveActions
    {
        public int ParametrizationCorrectiveActionID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }
    }
}