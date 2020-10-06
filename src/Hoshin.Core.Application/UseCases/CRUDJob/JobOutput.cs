using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDJob
{
    public class JobOutput
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
   
}
