using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction
{
    public class EditImpactDTO
    {
        public string Impact { get; set; }
        public int CorrectiveActionID { get; set; }
        public DateTime MaxDateImplementation { get; set; }
        public DateTime MaxDateEfficiencyEvaluation { get; set; }
    }
}
