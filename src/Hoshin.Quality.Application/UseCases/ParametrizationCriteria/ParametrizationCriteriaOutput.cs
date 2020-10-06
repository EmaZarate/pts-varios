using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria
{
    public sealed class ParametrizationCriteriaOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }

        public ParametrizationCriteriaOutput(int id, string name, string datatype)
        {
            this.Id = id;
            this.Name = name;
            this.DataType = datatype;
        }
    }
}
