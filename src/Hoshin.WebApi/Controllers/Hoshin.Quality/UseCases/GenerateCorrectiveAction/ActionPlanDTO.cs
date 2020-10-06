using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.GenerateCorrectiveAction
{
    public class ActionPlanDTO
    {
        public string WorkflowId { get; set; }
        public DateTime MaxDateEfficiencyEvaluation { get; set; }
        public DateTime MaxDateImplementation { get; set; }
        public string Impact { get; set; }
    }
}
