using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Core
{
    public class Administration : IClaim
    {
        public string Core { get; set; }

        public const string Add = "administration.add";
        public const string ReadAllModules = "administration.read.modules";
        public const string ReadAllViews = "administration.read.views";
        public const string ConfigurePlants = "administration.configure.plants";

    }
}
