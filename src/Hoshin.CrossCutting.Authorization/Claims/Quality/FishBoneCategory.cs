using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class FishBoneCategory:IClaim
    {
        public string Quality { get; set; }

        public const string AddFishBoneCategory = "fishbonecategory.add";
        public const string EditFishBoneCategory = "fishbonecategory.edit";
        public const string ReadFishBoneCategory = "fishbonecategory.read";
        public const string DeactivateFishBoneCategory = "fishbonecategory.deactivate";
        public const string ActivateFishBoneCategory = "fishbonecategory.activate";

    }
}
