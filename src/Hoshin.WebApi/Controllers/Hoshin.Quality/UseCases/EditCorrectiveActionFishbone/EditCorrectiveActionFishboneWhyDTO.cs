using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditCorrectiveActionFishbone
{
    public class EditCorrectiveActionFishboneWhyDTO
    {
        [JsonProperty("causeChildren")]
        public string Description { get; set; }
        public string SubChildren { get; set; }
        public int Index { get; set; }
    }
}
