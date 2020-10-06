using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.FindingType
{
    public class FindingTypeParametrization
    {
        [JsonProperty("id")]
        public int IdParametrization { get; set; }
        public int IdFindingType { get; set; }
        public string Value { get; set; }
        public ParametrizationCriteria.ParametrizationCriteria ParametrizationCriteria { get; set; }
    }
}
