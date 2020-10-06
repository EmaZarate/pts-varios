using Hoshin.Quality.Domain.FindingType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType
{
    public class FindingTypeOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public List<FindingTypeParametrization> Parametrizations { get; set; }

        public FindingTypeOutput()
        {

        }
        public FindingTypeOutput(int id, string name, string code, bool active, List<FindingTypeParametrization> parametrizations)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.Active = active;
            this.Parametrizations = parametrizations;
        }
    }
}
