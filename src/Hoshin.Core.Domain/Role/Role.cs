using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Domain.Role
{
    public class Role
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public IList<Claim.Claim> RoleClaims { get; set; }
        public bool Active { get; set; }
        public bool Basic { get; set; }
    }
}
