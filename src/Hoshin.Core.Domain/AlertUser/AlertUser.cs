using Hoshin.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Domain.AlertUser
{
    public class AlertUser
    {
        public int AlertID { get; set; }
        public string UsersID { get; set; }
        public int AlertUsersID { get; set; }
        public string Description { get; set; }
        public string AlertType { get; set; }
        public bool GenerateAlert { get; set; }
        

    }
}
