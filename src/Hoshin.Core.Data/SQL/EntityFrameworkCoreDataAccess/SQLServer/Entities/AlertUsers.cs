using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class AlertUsers
    {
        public AlertUsers()
        {
            
        }

        public int AlertUsersID { get; set; }
        public string UsersID { get; set; }
        public int AlertID { get; set; }
        public bool GenerateAlert { get; set; }
        public Alert Alert { get; set; }
        public Users Users { get; set; }

    }
}
