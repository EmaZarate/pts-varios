using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Core
{
    public class Claims : IClaim
    {
        public string Core { get; set; }

        public const string AddClaims = "claims.add";
        public const string EditClaims = "claims.edit";
        public const string ViewClaims = "claims.read";
        public const string DeactivateClaims = "claims.deactivate";
        public const string ActivateClaims = "claims.activate";
    }
}
