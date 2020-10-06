using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class FindingParametrizationCriteria : IClaim
    {
        public string Quality { get; set; }

        public const string AddParametrizationCriteria = "findingparametrizationcriteria.add";
        public const string EditParametrizationCriteria = "findingparametrizationcriteria.edit";
        public const string ReadParametrizationCriteria = "findingparametrizationcriteria.read";
        public const string DeactivateParametrizationCriteria = "findingparametrizationcriteria.deactivate";
        public const string ActivateParametrizationCriteria = "findingparametrizationcriteria.activate";
    }
}
