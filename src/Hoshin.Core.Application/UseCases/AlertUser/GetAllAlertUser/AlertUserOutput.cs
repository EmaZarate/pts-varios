using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.AlertUser.GetAllAlertUser
{
    public class AlertUserOutput
    {
        public int AlertUsersID { get; set; }
        public int AlertID { get; set; }
        public string UsersID { get; set; }
        public bool GenerateAlert { get; set; }
        public string Description { get; set; }
        public string AlertType { get; set; }
    }
}
