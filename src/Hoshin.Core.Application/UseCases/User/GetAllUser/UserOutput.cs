using Hoshin.Core.Domain.Job;
using Hoshin.Core.Domain.Plant;
using Hoshin.Core.Domain.Sector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.User.GetAllUser
{
    public sealed class UserOutput
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int PlantID { get; set; }
        public string Plant { get; set; }
        public int SectorID { get; set; }
        public string Sector { get; set; }
        public int JobID { get; set; }
        public string Job { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string base64Profile { get; set;}
        public bool Active { get; set; }
        public IList<string> Roles { get; set; }

        public UserOutput(){}
        public UserOutput(string id, string name, string surname)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
        }

        public void SetPlantSectorJob(Plant plant, Sector sector, Job job)
        {
            this.Plant = plant.Name;
            this.Sector = sector.Name;
            this.Job = job.Name;
        }
    }
}
