using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Domain.Company
{
    public class Company
    {
        public Company()
        {

        }

        public Company(int id, string businessName, string cuit, string address, string phoneNumber, string logo)
        {
            CompanyID = id;
            BusinessName = businessName;
            CUIT = cuit;
            Address = address;
            PhoneNumber = phoneNumber;
            Logo = logo;
        }

        public Company(string businessName, string cuit, string address, string phoneNumber, string logo)
        {
            BusinessName = businessName;
            CUIT = cuit;
            Address = address;
            PhoneNumber = phoneNumber;
            Logo = logo;
        }

        public int CompanyID { get; set; }
        public string BusinessName { get; set; }
        public string CUIT { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Logo { get; set; }
    }
}
