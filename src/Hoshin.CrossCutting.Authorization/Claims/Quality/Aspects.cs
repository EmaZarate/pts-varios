using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class Aspects: IClaim
    {
        public string Quality { get; set; }

        public const string AddAspects = "aspects.add";
        public const string EditAspects = "aspects.edit";
        public const string ReadAspects = "aspects.read";
        public const string DeactivateAspects = "aspects.deactivate";
        public const string ActivateAspects = "aspects.activate";
    }
}
