using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class TaskEvidences
    {
        public int TaskEvidencesID { get; set; }
        public int TaskID { get; set; }
        public Tasks Task { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
     

    }
}