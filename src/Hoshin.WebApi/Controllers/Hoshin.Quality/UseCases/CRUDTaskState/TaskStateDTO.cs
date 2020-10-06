using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDTaskState
{
    public class TaskStateDTO
    {
        public int TaskStateID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }
    }
}
