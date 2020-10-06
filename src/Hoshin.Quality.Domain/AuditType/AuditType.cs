using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.AuditType
{
    public class AuditType
    {
        public AuditType(string code, string name, bool active)
        {
            Code = code;
            Name = name;
            Active = active;
        }

        public AuditType(string code, string name, bool active, int id)
        {
            Code = code;
            Name = name;
            Active = active;
            Id = id;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
