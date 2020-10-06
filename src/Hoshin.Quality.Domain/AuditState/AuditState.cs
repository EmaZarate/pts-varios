using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.AuditState
{
    public class AuditState
    {
        public int AuditStateID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }
}
