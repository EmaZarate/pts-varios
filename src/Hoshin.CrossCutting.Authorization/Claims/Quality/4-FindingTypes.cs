using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class FindingTypes : IClaim
    {
        public string Quality { get; set; }

        public const string AddTypes = "findingtypes.add";
        public const string EditTypes = "findingtypes.edit";
        public const string ReadTypes = "findingtypes.read";
        public const string DeactivateTypes = "findingtypes.deactivate";
        public const string ActivateTypes = "findingtypes.activate";
        public const string ConfigureTypes = "findingtypes.configure";
        public const string EditConfigureTypes = "findingtypes.edit.configure";
        public const string ReadConfigureTypes = "findingtypes.read.configure";
        public const string DeleteConfigureTypes = "findingtypes.delete.configure";
    }
}
