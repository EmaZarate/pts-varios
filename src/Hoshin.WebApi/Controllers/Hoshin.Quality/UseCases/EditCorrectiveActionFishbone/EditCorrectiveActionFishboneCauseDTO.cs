using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditCorrectiveActionFishbone
{
    public class EditCorrectiveActionFishboneCauseDTO
    {
        [JsonProperty("Cause")]
        public List<EditCorrectiveActionFishboneWhyDTO> Whys { get; set; }
        public List<CauseCoordsDTO> Coords { get; set; }
        [JsonProperty("spineChildName")]
        public string Name { get; set; }
    }
}
