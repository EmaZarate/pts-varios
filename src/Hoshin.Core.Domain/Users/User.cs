using Hoshin.Core.Domain.Interfaces;
using System.Collections.Generic;

namespace Hoshin.Core.Domain.Users
{
    public sealed class User : IEntity
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return LastName + ", " + FirstName; } }
        public string PictureUrl { get; set; }
        public string MicrosoftGraphId { get; set; }
        public string Email { get; set; }
        public JobSectorPlant JobSectorPlant { get; set; }
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public int JobID { get; set; }
        public string PhoneNumber { get; set;}
        public string Address { get; set; }
        public byte[] ImageProfile { get; set; }
        public string base64Profile { get; set; }
        public bool Active { get; set; }
        public IList<string> Roles { get; set; }

        public User() { }

        public User(string userName, string password, int jobId, int sectorId, int plantId, string name, string surname, bool active)
        {
            this.Username = userName;
            this.Password = password;
            this.JobID = jobId;
            this.SectorID = sectorId;
            this.PlantID = plantId;
            this.FirstName = name;
            this.LastName = surname;
            this.Active = active;
        }

        public User(string id, string userName, string password, int jobId, int sectorId, int plantId, string name, string surname,string address,string phoneNumber,string base64Profile, bool active)
        {
            this.Id = id;
            this.Username = userName;
            this.Password = password;
            this.JobID = jobId;
            this.SectorID = sectorId;
            this.PlantID = plantId;
            this.FirstName = name;
            this.LastName = surname;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.base64Profile = base64Profile;
            this.Active = active;
        }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User(string username, string password, bool active)
        {
            Username = username;
            Password = password;
            Active = active;
        }

        public User(string id, string username, string firstname, string lastname, string msftgid, string email)
        {
            this.Id = id;
            this.Username = username;
            this.FirstName = lastname;
            this.MicrosoftGraphId = msftgid;
            this.Email = email;
        }


    }
}
