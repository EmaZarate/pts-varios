using System;
using System.Collections.Generic;
using System.Text;
namespace Hoshin.Core.Domain.Sector
{
    public class Sector
    {
        public Sector()
        {

        }
        public Sector(int id, string name, string code, string description, bool active)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
            Active = active;
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string ResposibleUserPlantSector { get; set; }

        public List<Domain.Job.Job> jobs { get; set; }
    }
}
