using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDUser
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public int JobID { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Base64Profile { get; set; }
        public List<string> Roles { get; set; }
        public byte[] ImageProfile { get; set; }
        public string Email { get; set; }
        public string PictureUrl { get; set; }
        public string LastName { get; set; }
        public string MicrosoftGraphId { get; set; }
        public bool Active { get; set; }
    }
}
