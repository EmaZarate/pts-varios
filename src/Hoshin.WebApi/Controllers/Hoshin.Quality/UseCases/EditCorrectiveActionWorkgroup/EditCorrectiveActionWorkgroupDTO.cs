using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditCorrectiveActionWorkgroup
{
    public class EditCorrectiveActionWorkgroupDTO
    {
        public int CorrectiveActionID { get; set; }
        public List<string> UsersID { get; set; }
    }
}
