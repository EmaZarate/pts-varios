using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class AspectState: IClaim
    {
        public string Quality { get; set; }

        public const string AddAspectState = "aspectstate.add";
        public const string EditAspectState = "aspectstate.edit";
        public const string ReadAspectState = "aspectstate.read";
        public const string ActivateAspectState = "aspectstate.activate";
        public const string DeactivateAspectState = "aspectstate.deactivate";
    }
}
