using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.Role
{
    public class ClaimOutput
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
