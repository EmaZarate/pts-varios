using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
   public  class AlertRol
    {
       
        public string RolID { get; set; }
        public int AlertID { get; set; }

        public Alert Alert { get; set; }

        public Roles Rol{ get; set; }
    }


}
