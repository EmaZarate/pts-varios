using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Core
{
    public class User : IClaim
    {
        public string Core { get; set; }

        public const string AddUser = "user.add";
        public const string EditUser = "user.edit";
        public const string ViewUser = "user.read";
        public const string DeactivateUser = "user.deactivate";
        public const string ActivateUser = "user.activate";
        public const string EditProfile = "user.editprofile";
    }
}
