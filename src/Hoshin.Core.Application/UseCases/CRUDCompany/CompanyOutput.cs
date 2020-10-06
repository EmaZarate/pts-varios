using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDCompany
{
    public class CompanyOutput
    {
        public int CompanyID { get; set; }
        public string BusinessName { get; set; }
        public string CUIT { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Logo { get; set; }
    }
}
