using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Core
{
    public class Company : IClaim
    {
        public string Core { get; set; }

        public const string AddCompany = "company.add";
        public const string EditCompany = "company.edit";
        public const string ReadCompany = "company.read";
    }
}
