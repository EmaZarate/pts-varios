﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual Users User { get; set; }
        public virtual Roles Role { get; set; }
    }
}
