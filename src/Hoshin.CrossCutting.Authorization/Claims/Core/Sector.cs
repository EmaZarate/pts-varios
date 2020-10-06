using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Core
{
    public class Sector : IClaim
    {
        public string Core { get; set; }

        public const string AddSector = "sector.add";
        public const string EditSector = "sector.edit";
        public const string ViewSector = "sector.read";
        public const string DeactivateSector = "sector.deactivate";
        public const string ActivateSector = "sector.activate";
    }
}
