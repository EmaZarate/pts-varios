using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDJobs
{
    public class JobDTO
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
