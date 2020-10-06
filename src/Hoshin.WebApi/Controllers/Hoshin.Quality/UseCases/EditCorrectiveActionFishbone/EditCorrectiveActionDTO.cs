using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditCorrectiveActionFishbone
{
    public class EditCorrectiveActionDTO
    {
        [JsonProperty("data")]
        public List<EditCorrectiveActionFishboneDTO> correctiveActionFishbone { get; set; }
        public string RootReason { get; set; }
    }
}
