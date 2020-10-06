using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDCompanies
{
    public class CompanyDTO
    {
        public int CompanyID { get; set; }
        public string BusinessName { get; set; }
        public string CUIT { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Logo { get; set; }
    }
}
