using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDParametrizationCorrectiveAction
{
    public class ParametrizationCorrectiveActionDTO
    {
        public int ParametrizationCorrectiveActionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int value { get; set; }
    }
}
