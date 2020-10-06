using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class AspectStates
    {
        public int AspectStateID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }
    }
}
