using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class CorrectiveActions: IClaim
    {
        public string Quality { get; set; }

        public const string Schedule = "correctiveactions.schedule";
        public const string Delete = "correctiveactions.delete";
        public const string Planning = "correctiveactions.planning";
        public const string Export = "correctiveactions.export";
        public const string Evaluate = "correctiveactions.evaluate";
        public const string ReassginDirectly = "correctiveactions.reassign.direct";
        public const string RequestPlanningDueDateExtention = "correctiveactions.request.planningduedateextention";
        public const string RequestEvaluateDueDateExtention = "correctiveactions.request.evaluateduedateextention";
        public const string ExtendPlanningDueDate = "correctiveactions.extend.plannigduedate";
        public const string ExtendEvaluateDueDate = "correctiveactions.extend.evaluateduedate";
        public const string Read = "correctiveactions.read";
    }
}
