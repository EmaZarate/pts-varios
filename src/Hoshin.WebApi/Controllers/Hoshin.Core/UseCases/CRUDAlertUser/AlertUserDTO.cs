using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDAlertUser
{
    public class AlertUserDTO
    {
        public int AlertID { get; set; }
        public string UsersID { get; set; }
        public int AlertUsersID { get; set; }
        public string Description { get; set; }
        public string AlertType { get; set; }
        public bool GenerateAlert { get; set; }
    }
}
