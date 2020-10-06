using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDRole
{
    public class RoleDTO
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Basic { get; set; }
        public List<string> Claims { get; set; }
    }
}
