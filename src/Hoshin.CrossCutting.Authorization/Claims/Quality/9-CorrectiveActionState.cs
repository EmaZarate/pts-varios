using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class CorrectiveActionState:IClaim
    {
        public string Quality { get; set; }

        public const string AddCorrectiveActionState = "correctiveactionstate.add";
        public const string EditCorrectiveActionState = "correctiveactionstate.edit";
        public const string ReadCorrectiveActionState = "correctiveactionstate.read";
        public const string DeactivateCorrectiveActionState = "correctiveactionstate.deactivate";
        public const string ActivateCorrectiveActionState = "correctiveactionstate.activate";
    }
}
