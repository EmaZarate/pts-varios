using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class FindingStates : IClaim
    {
        public string Quality { get; set; }

        public const string AddStates = "findingstates.add";
        public const string EditStates = "findingstates.edit";
        public const string ReadStates = "findingstates.read";
        public const string DeactivateStates = "findingstates.deactivate";
        public const string ActivateStates = "findingstates.activate";
    }
}
