using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Core
{
    public class Plant : IClaim
    {
        public string Core { get; set; }

        public const string AddPlant = "plant.add";
        public const string EditPlant = "plant.edit";
        public const string ViewPlant = "plant.read";
        public const string DeactivatePlant = "plant.deactivate";
        public const string ActivatePlant = "plant.activate";
    }
}
