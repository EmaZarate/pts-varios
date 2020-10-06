using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class Roles : IdentityRole
    {
        public Roles() : base()
        {

        }
        public Roles(string roleName) : base(roleName)
        {

        }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        public virtual bool Active { get; set; }
        public virtual bool Basic { get; set; }
    }
}
