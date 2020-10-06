using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class Aspects
    {
        public int AspectID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int StandardID { get; set; }
        public bool Active { get; set; }
        public Standards Standard { get; set; }
    }
}
