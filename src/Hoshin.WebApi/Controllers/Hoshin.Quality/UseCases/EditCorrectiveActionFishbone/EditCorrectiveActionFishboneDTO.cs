using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditCorrectiveActionFishbone
{
    public class EditCorrectiveActionFishboneDTO
    {
        public int CorrectiveActionID { get; set; }
        [JsonProperty("CategoryId")]
        public int FishboneID { get; set; }

        [JsonProperty("boneSpineChild")]
        public List<EditCorrectiveActionFishboneCauseDTO> Causes { get; set; }
    }
}
