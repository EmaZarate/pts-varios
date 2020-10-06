using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Domain.Job
{
    public class Job
    {
        public Job()
        {

        }

        public Job(int id, string name, string code, bool active)
        {
            Id = id;
            Name = name;
            Code = code;
            Active = active;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
