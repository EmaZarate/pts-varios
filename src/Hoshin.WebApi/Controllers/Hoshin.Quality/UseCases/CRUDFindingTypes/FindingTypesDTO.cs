using Hoshin.Quality.Domain.FindingType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFindingTypes
{
    public class FindingTypesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        [JsonProperty("parametrization")]
        public List<FindingTypeParametrization> Parametrizations{ get; set; }
    }
}
