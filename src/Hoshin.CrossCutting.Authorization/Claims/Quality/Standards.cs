using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class Standards : IClaim
    {
        public string Quality { get; set; }

        public const string AddStandards = "standrads.add";
        public const string EditStandrads = "standrads.edit";
        public const string ReadStandrads = "standrads.read";
        public const string DeactivateStandrads = "standrads.deactivate";
        public const string ActivateStandrads = "standrads.activate";
    }
}

