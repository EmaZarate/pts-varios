using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class Alert
    {
        public int AlertID { get; set; }
        public string Description { get; set; }
        public string AlertType { get; set; }
        public virtual ICollection<AlertUsers> AlertUsers { get; set; }

    }
}
