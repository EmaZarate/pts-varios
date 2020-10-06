using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class CorrectiveActionParametrization:IClaim
    {
        public string Quality { get; set; }

        public const string AddCorrectiveActionParametrization = "correctiveactionparametrization.add";
        public const string EditCorrectiveActionParametrization = "correctiveactionparametrization.edit";
        public const string ReadCorrectiveActionParametrization = "correctiveactionparametrization.read";
    }
}
