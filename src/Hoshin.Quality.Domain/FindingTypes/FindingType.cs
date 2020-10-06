using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.FindingType
{
    public class FindingType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public List<FindingTypeParametrization> Parametrizations { get; set; }

        public FindingType()
        {

        }
        public FindingType(string name, string code, bool active, List<FindingTypeParametrization> findingTypeParametrizations)
        {
            this.Name = name;
            this.Code = code;
            this.Active = active;
            this.Parametrizations = findingTypeParametrizations;
        }
    }
}
