using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.Role
{
    public class RoleOutput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<ClaimOutput> RoleClaims { get; set; }
        public bool Active { get; set; }
        public bool Basic { get; set; }
    }
}
