using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class Fishbone
    {
        public int FishboneID { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public bool Active { get; set;}
    }
}
