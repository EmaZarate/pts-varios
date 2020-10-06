using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Domain.Claim
{
    public class Claim
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
