using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Core
{
    public class Roles :IClaim
    {
        public string Core { get; set; }

        public const string AddRole = "role.add";
        public const string EditRole = "role.edit";
        public const string ViewRole = "role.read";
        public const string DeactivateRoles = "role.deactivate";
        public const string ActivateRole = "role.activate";
    }
}
